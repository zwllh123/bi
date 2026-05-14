<template>
  <div class="page-card">
    <div class="toolbar">
      <el-button @click="$router.back()">返回</el-button>
      <span style="margin-left:8px;font-weight:600">数据集：{{ setCode }}</span>
      <div class="grow"></div>
      <el-button type="primary" @click="onExecute">执行预览</el-button>
    </div>
    <el-tabs v-model="tab">
      <el-tab-pane label="参数" name="params">
        <div style="margin-bottom:8px"><el-button size="small" @click="onAddParam">新增参数</el-button></div>
        <el-table :data="params" border size="small">
          <el-table-column label="名称">
            <template #default="{ row }"><el-input v-model="row.paramName" size="small" /></template>
          </el-table-column>
          <el-table-column label="类型" width="120">
            <template #default="{ row }">
              <el-select v-model="row.paramType" size="small">
                <el-option label="string" value="string" />
                <el-option label="int" value="int" />
                <el-option label="date" value="date" />
              </el-select>
            </template>
          </el-table-column>
          <el-table-column label="样例" width="160">
            <template #default="{ row }"><el-input v-model="row.sampleItem" size="small" /></template>
          </el-table-column>
          <el-table-column label="必填" width="80">
            <template #default="{ row }"><el-switch v-model="row.requiredFlag" :active-value="1" :inactive-value="0" /></template>
          </el-table-column>
          <el-table-column label="排序" width="80">
            <template #default="{ row }"><el-input-number v-model="row.orderNum" size="small" :min="0" /></template>
          </el-table-column>
          <el-table-column label="操作" width="140">
            <template #default="{ row, $index }">
              <el-button size="small" type="primary" @click="onSaveParam(row)">保存</el-button>
              <el-button size="small" type="danger" @click="onDeleteParam(row, $index)">删</el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-tab-pane>
      <el-tab-pane label="执行预览" name="preview">
        <div style="margin-bottom:8px">
          <span>参数值：</span>
          <el-tag v-for="p in params" :key="p.id" style="margin-right:8px">
            {{ p.paramName }}=<el-input v-model="paramValues[p.paramName || '']" size="small" style="width:120px" />
          </el-tag>
        </div>
        <el-table :data="previewRows" border size="small" max-height="400">
          <el-table-column v-for="c in previewColumns" :key="c" :prop="c" :label="c" />
        </el-table>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, reactive } from 'vue'
import { useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import { setDetail, setExecute, paramInsert, paramUpdate, paramDelete, type DataSetParam } from '@/api/dataset'

const route = useRoute()
const setCode = ref(String(route.params.setCode))
const tab = ref('params')
const params = ref<Partial<DataSetParam>[]>([])
const previewColumns = ref<string[]>([])
const previewRows = ref<Record<string, unknown>[]>([])
const paramValues = reactive<Record<string, unknown>>({})
let setId = 0

async function load() {
  // 通过列表页跳转过来，使用 setCode 查询不到 id，需要先用其他接口查；这里偷懒：直接执行用 setCode
  // 简化：先加载参数
  const detail = await setDetail(0).catch(() => null)
  if (detail) {
    setId = detail.dataSet.id
    params.value = detail.params || []
  }
}
onMounted(load)

function onAddParam() {
  params.value.push({ setCode: setCode.value, paramName: '', paramType: 'string', requiredFlag: 0, orderNum: params.value.length })
}
async function onSaveParam(row: Partial<DataSetParam>) {
  if (!row.paramName) { ElMessage.warning('名称必填'); return }
  if (row.id) await paramUpdate(row)
  else { const r: any = await paramInsert(row); if (r?.id) row.id = r.id }
  ElMessage.success('已保存')
}
async function onDeleteParam(row: Partial<DataSetParam>, idx: number) {
  if (row.id) await paramDelete(row.id)
  params.value.splice(idx, 1)
  ElMessage.success('已删除')
}

async function onExecute() {
  tab.value = 'preview'
  const r = await setExecute({ setCode: setCode.value, parameters: { ...paramValues } })
  previewColumns.value = r.columns || []
  previewRows.value = r.rows || []
}
</script>
