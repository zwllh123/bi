<template>
  <div class="designer-root">
    <!-- 左：组件工具栏 -->
    <aside class="designer-left">
      <div class="tool-group">
        <div class="tool-group-title">基础组件</div>
        <div v-for="d in widgetDefs" :key="d.type" class="tool-item" draggable="true"
          @dragstart="onDragStart($event, d.type)" @click="onAddWidget(d.type)">
          <component :is="iconMap[d.icon]" style="width:16px;height:16px" />
          <span style="margin-left:8px">{{ d.name }}</span>
        </div>
      </div>
      <div class="tool-group">
        <div class="tool-group-title">操作</div>
        <div class="tool-item" @click="onClearAll">
          <component :is="iconMap.IconDelete" style="width:16px;height:16px" />
          <span style="margin-left:8px">清空画布</span>
        </div>
      </div>
    </aside>

    <!-- 中央 -->
    <section class="designer-center">
      <div class="designer-topbar">
        <el-button size="small" @click="$router.push('/report')">返回</el-button>
        <span style="margin-left:12px;font-weight:600">{{ reportCode }}</span>
        <span style="color:#888;margin-left:12px">画布尺寸：</span>
        <el-input-number v-model="dashboard.width" size="small" :min="200" :max="9999" controls-position="right" />
        <span style="color:#888">×</span>
        <el-input-number v-model="dashboard.height" size="small" :min="200" :max="9999" controls-position="right" />
        <div class="grow" style="flex:1"></div>
        <el-button size="small" @click="onPreview">预览</el-button>
        <el-button size="small" type="primary" @click="onSave" :loading="saving">保存</el-button>
      </div>
      <div class="designer-canvas-wrap" @dragover.prevent @drop="onDrop">
        <div ref="canvasEl" class="designer-canvas" :style="canvasStyle" @click.self="selectedId = null">
          <div v-for="w in widgets" :key="w.tempId"
            class="widget-box" :class="{ selected: selectedId === w.tempId }"
            :style="boxStyle(w)" @mousedown.stop="onWidgetMouseDown($event, w)" @click.stop="selectedId = w.tempId">
            <WidgetRender :widget="w" />
            <div v-if="selectedId === w.tempId" class="resize-handle" @mousedown.stop="onResizeMouseDown($event, w)"></div>
          </div>
        </div>
      </div>
    </section>

    <!-- 右：属性面板 -->
    <aside class="designer-right">
      <div v-if="!selectedWidget">
        <div class="prop-section">
          <div class="prop-section-title">画布属性</div>
          <el-form label-width="80px" size="small">
            <el-form-item label="标题"><el-input v-model="dashboard.title" /></el-form-item>
            <el-form-item label="宽度"><el-input-number v-model="dashboard.width" :min="200" :max="9999" /></el-form-item>
            <el-form-item label="高度"><el-input-number v-model="dashboard.height" :min="200" :max="9999" /></el-form-item>
            <el-form-item label="背景色"><el-color-picker v-model="dashboard.backgroundColor" show-alpha /></el-form-item>
            <el-form-item label="刷新(秒)"><el-input-number v-model="dashboard.refreshSeconds" :min="0" /></el-form-item>
          </el-form>
        </div>
      </div>
      <div v-else>
        <div class="prop-section">
          <div class="prop-section-title">基础信息</div>
          <el-form label-width="80px" size="small">
            <el-form-item label="类型"><el-input :model-value="selectedWidget.type" disabled /></el-form-item>
            <el-form-item label="X"><el-input-number v-model="selectedWidget.position.x" :min="0" /></el-form-item>
            <el-form-item label="Y"><el-input-number v-model="selectedWidget.position.y" :min="0" /></el-form-item>
            <el-form-item label="宽"><el-input-number v-model="selectedWidget.position.width" :min="20" /></el-form-item>
            <el-form-item label="高"><el-input-number v-model="selectedWidget.position.height" :min="20" /></el-form-item>
          </el-form>
        </div>
        <div class="prop-section">
          <div class="prop-section-title">样式</div>
          <el-form label-width="80px" size="small">
            <template v-if="['line','bar','pie','table'].includes(selectedWidget.type)">
              <el-form-item label="显示标题"><el-switch v-model="selectedWidget.setup.showTitle" /></el-form-item>
              <el-form-item label="标题"><el-input v-model="selectedWidget.setup.title" /></el-form-item>
            </template>
            <template v-if="selectedWidget.type === 'text'">
              <el-form-item label="内容"><el-input v-model="selectedWidget.setup.content" type="textarea" :rows="3" /></el-form-item>
              <el-form-item label="字号"><el-input-number v-model="selectedWidget.setup.fontSize" :min="8" :max="200" /></el-form-item>
              <el-form-item label="文字色"><el-color-picker v-model="selectedWidget.setup.textColor" /></el-form-item>
            </template>
            <template v-if="selectedWidget.type === 'image'">
              <el-form-item label="图片URL"><el-input v-model="selectedWidget.setup.url" /></el-form-item>
            </template>
            <el-form-item label="背景色"><el-color-picker v-model="selectedWidget.setup.bgColor" show-alpha /></el-form-item>
          </el-form>
        </div>
        <div class="prop-section" v-if="['line','bar','pie','table'].includes(selectedWidget.type)">
          <div class="prop-section-title">数据</div>
          <el-form label-width="80px" size="small">
            <el-form-item label="数据来源">
              <el-radio-group v-model="selectedWidget.data.mode">
                <el-radio value="static">静态</el-radio>
                <el-radio value="dataset">数据集</el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item v-if="selectedWidget.data.mode === 'dataset'" label="数据集">
              <el-input v-model="selectedWidget.data.setCode" placeholder="请输入数据集编码" />
            </el-form-item>
            <template v-if="selectedWidget.type === 'pie'">
              <el-form-item label="名称字段"><el-input v-model="selectedWidget.data.nameField" /></el-form-item>
              <el-form-item label="数值字段"><el-input v-model="selectedWidget.data.valueField" /></el-form-item>
            </template>
            <template v-else-if="selectedWidget.type === 'line' || selectedWidget.type === 'bar'">
              <el-form-item label="X 字段"><el-input v-model="selectedWidget.data.xField" /></el-form-item>
              <el-form-item label="Y 字段"><el-input v-model="selectedWidget.data.yField" /></el-form-item>
            </template>
            <el-form-item v-if="selectedWidget.data.mode === 'static'" label="静态数据">
              <el-input type="textarea" :rows="6" :model-value="JSON.stringify(selectedWidget.data.staticData, null, 2)"
                @change="onStaticDataChange" />
            </el-form-item>
          </el-form>
        </div>
        <div class="prop-section">
          <el-button size="small" type="danger" @click="onDeleteWidget" style="width:100%">删除当前组件</el-button>
        </div>
      </div>
    </aside>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, reactive, onMounted, markRaw } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import {
  EditPen, Picture, TrendCharts, Histogram, PieChart, Grid, Delete,
} from '@element-plus/icons-vue'
import WidgetRender from './WidgetRender.vue'
import { WIDGET_LIST, widgetFromDef, type WidgetModel } from './widgets'
import { dashLoad, dashSave, dashSaveWidgets, type Dashboard } from '@/api/report'

const route = useRoute()
const router = useRouter()
const reportCode = String(route.params.reportCode)

const iconMap: Record<string, unknown> = {
  IconEditPen: markRaw(EditPen),
  IconPicture: markRaw(Picture),
  IconTrendCharts: markRaw(TrendCharts),
  IconHistogram: markRaw(Histogram),
  IconPieChart: markRaw(PieChart),
  IconGrid: markRaw(Grid),
  IconDelete: markRaw(Delete),
}

const widgetDefs = WIDGET_LIST
const widgets = ref<WidgetModel[]>([])
const selectedId = ref<string | null>(null)
const saving = ref(false)
const canvasEl = ref<HTMLElement | null>(null)

const dashboard = reactive<Partial<Dashboard>>({
  reportCode, title: '', width: 1920, height: 1080,
  backgroundColor: '#1e1e2f', refreshSeconds: 0, enableFlag: 1, sort: 0,
})

const canvasStyle = computed(() => ({
  width: dashboard.width + 'px',
  height: dashboard.height + 'px',
  background: dashboard.backgroundColor || '#1e1e2f',
}))

const selectedWidget = computed(() => widgets.value.find(w => w.tempId === selectedId.value))

function boxStyle(w: WidgetModel) {
  return {
    left: w.position.x + 'px',
    top: w.position.y + 'px',
    width: w.position.width + 'px',
    height: w.position.height + 'px',
  }
}

function onAddWidget(type: string) {
  const def = WIDGET_LIST.find(d => d.type === type)
  if (!def) return
  const w = widgetFromDef(def, reportCode, widgets.value.length)
  widgets.value.push(w)
  selectedId.value = w.tempId
}

function onDragStart(e: DragEvent, type: string) {
  e.dataTransfer?.setData('widgetType', type)
}
function onDrop(e: DragEvent) {
  const type = e.dataTransfer?.getData('widgetType')
  if (!type || !canvasEl.value) return
  const rect = canvasEl.value.getBoundingClientRect()
  const def = WIDGET_LIST.find(d => d.type === type)
  if (!def) return
  const w = widgetFromDef(def, reportCode, widgets.value.length)
  w.position.x = Math.max(0, e.clientX - rect.left - w.position.width / 2)
  w.position.y = Math.max(0, e.clientY - rect.top - w.position.height / 2)
  widgets.value.push(w)
  selectedId.value = w.tempId
}

function onClearAll() {
  widgets.value = []
  selectedId.value = null
}
function onDeleteWidget() {
  if (!selectedId.value) return
  const i = widgets.value.findIndex(w => w.tempId === selectedId.value)
  if (i >= 0) widgets.value.splice(i, 1)
  selectedId.value = null
}

function onStaticDataChange(v: string) {
  if (!selectedWidget.value) return
  try {
    selectedWidget.value.data.staticData = JSON.parse(v)
  } catch {
    ElMessage.warning('JSON 格式有误')
  }
}

// 拖动 / 缩放
function onWidgetMouseDown(e: MouseEvent, w: WidgetModel) {
  selectedId.value = w.tempId
  const startX = e.clientX, startY = e.clientY
  const ox = w.position.x, oy = w.position.y
  function move(ev: MouseEvent) {
    w.position.x = Math.max(0, ox + ev.clientX - startX)
    w.position.y = Math.max(0, oy + ev.clientY - startY)
  }
  function up() { document.removeEventListener('mousemove', move); document.removeEventListener('mouseup', up) }
  document.addEventListener('mousemove', move)
  document.addEventListener('mouseup', up)
}

function onResizeMouseDown(e: MouseEvent, w: WidgetModel) {
  const startX = e.clientX, startY = e.clientY
  const ow = w.position.width, oh = w.position.height
  function move(ev: MouseEvent) {
    w.position.width = Math.max(40, ow + ev.clientX - startX)
    w.position.height = Math.max(40, oh + ev.clientY - startY)
  }
  function up() { document.removeEventListener('mousemove', move); document.removeEventListener('mouseup', up) }
  document.addEventListener('mousemove', move)
  document.addEventListener('mouseup', up)
}

// 加载 / 保存
async function load() {
  try {
    const detail = await dashLoad(reportCode)
    if (detail?.dashboard) {
      Object.assign(dashboard, detail.dashboard)
    }
    widgets.value = (detail?.widgets || []).map((w, i) => {
      let pos = { x: 0, y: 0, width: 320, height: 240 }
      let setup: WidgetModel['setup'] = {}
      let data: WidgetModel['data'] = { mode: 'static' }
      try { if (w.position) pos = { ...pos, ...JSON.parse(w.position) } } catch { /* ignore */ }
      try { if (w.setup) setup = JSON.parse(w.setup) } catch { /* ignore */ }
      try { if (w.data) data = JSON.parse(w.data) } catch { /* ignore */ }
      return {
        id: w.id, tempId: 'w_' + w.id, reportCode, type: w.type,
        position: pos, setup, data, sort: w.sort ?? i,
      }
    })
  } catch {
    // 新建报表时 load 可能 404，忽略
  }
}

async function onSave() {
  saving.value = true
  try {
    await dashSave(dashboard)
    const payload = widgets.value.map((w, i) => ({
      id: w.id, reportCode, type: w.type, sort: i, enableFlag: 1,
      position: JSON.stringify(w.position),
      setup: JSON.stringify(w.setup),
      data: JSON.stringify(w.data),
    }))
    await dashSaveWidgets({ reportCode, widgets: payload })
    ElMessage.success('已保存')
  } finally {
    saving.value = false
  }
}

function onPreview() {
  window.open(`/preview/${reportCode}`, '_blank')
}

onMounted(load)
</script>
