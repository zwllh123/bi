<template>
  <div ref="root" :style="containerStyle" class="widget-render">
    <div v-if="widget.setup.showTitle && (widget.type !== 'text' && widget.type !== 'image')" class="widget-title">
      {{ widget.setup.title }}
    </div>
    <!-- 文本 -->
    <div v-if="widget.type === 'text'" class="widget-text"
      :style="{ fontSize: (widget.setup.fontSize || 16) + 'px', color: widget.setup.textColor || '#fff' }">
      {{ widget.setup.content }}
    </div>
    <!-- 图片 -->
    <img v-else-if="widget.type === 'image'" :src="widget.setup.url" class="widget-image" />
    <!-- 表格 -->
    <div v-else-if="widget.type === 'table'" class="widget-table-wrap">
      <table class="widget-table">
        <thead>
          <tr><th v-for="c in tableColumns" :key="c">{{ c }}</th></tr>
        </thead>
        <tbody>
          <tr v-for="(r, i) in tableRows" :key="i">
            <td v-for="c in tableColumns" :key="c">{{ r[c] }}</td>
          </tr>
        </tbody>
      </table>
    </div>
    <!-- 图表 -->
    <div v-else ref="chartEl" class="widget-chart"></div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, onBeforeUnmount, ref, watch, nextTick } from 'vue'
import * as echarts from 'echarts'
import type { WidgetModel } from './widgets'
import { setExecute } from '@/api/dataset'

const props = defineProps<{ widget: WidgetModel }>()
const root = ref<HTMLElement | null>(null)
const chartEl = ref<HTMLElement | null>(null)
let chart: echarts.ECharts | null = null

const containerStyle = computed(() => ({
  width: '100%',
  height: '100%',
  background: props.widget.setup.bgColor || 'transparent',
  color: props.widget.setup.textColor || '#fff',
  padding: '4px',
  boxSizing: 'border-box' as const,
  overflow: 'hidden',
  position: 'relative' as const,
}))

const liveData = ref<Record<string, unknown>[]>([])

async function loadData() {
  if (props.widget.data.mode === 'dataset' && props.widget.data.setCode) {
    try {
      const r = await setExecute({ setCode: props.widget.data.setCode, parameters: props.widget.data.parameters || {} })
      liveData.value = r.rows || []
    } catch {
      liveData.value = []
    }
  } else {
    liveData.value = props.widget.data.staticData || []
  }
  renderChart()
}

const tableRows = computed(() => liveData.value)
const tableColumns = computed(() => {
  const first = liveData.value[0]
  return first ? Object.keys(first) : []
})

function buildOption() {
  const w = props.widget
  const rows = liveData.value
  const colors = w.setup.colors || ['#5470c6']
  if (w.type === 'pie') {
    const nf = w.data.nameField || 'name'
    const vf = w.data.valueField || 'value'
    return {
      color: colors,
      tooltip: { trigger: 'item' },
      legend: { textStyle: { color: '#ccc' }, bottom: 0 },
      series: [{
        type: 'pie', radius: ['40%', '65%'],
        data: rows.map(r => ({ name: String(r[nf]), value: Number(r[vf]) })),
        label: { color: '#ccc' },
      }],
    }
  }
  const xf = w.data.xField || 'name'
  const yf = w.data.yField || 'value'
  return {
    color: colors,
    tooltip: { trigger: 'axis' },
    grid: { left: 40, right: 20, top: 30, bottom: 30 },
    xAxis: {
      type: 'category',
      data: rows.map(r => r[xf]),
      axisLabel: { color: '#ccc' },
      axisLine: { lineStyle: { color: '#666' } },
    },
    yAxis: {
      type: 'value',
      axisLabel: { color: '#ccc' },
      splitLine: { lineStyle: { color: '#333' } },
    },
    series: [{
      type: w.type === 'line' ? 'line' : 'bar',
      data: rows.map(r => Number(r[yf])),
      smooth: true,
    }],
  }
}

function renderChart() {
  if (props.widget.type === 'text' || props.widget.type === 'image' || props.widget.type === 'table') return
  nextTick(() => {
    if (!chartEl.value) return
    if (!chart) chart = echarts.init(chartEl.value)
    chart.setOption(buildOption() as never, true)
    chart.resize()
  })
}

const resizeObserver = new ResizeObserver(() => chart?.resize())
onMounted(() => {
  loadData()
  if (root.value) resizeObserver.observe(root.value)
})
onBeforeUnmount(() => {
  resizeObserver.disconnect()
  chart?.dispose()
})

watch(() => [props.widget.setup, props.widget.data, props.widget.type, props.widget.position.width, props.widget.position.height], () => {
  loadData()
}, { deep: true })
</script>

<style scoped>
.widget-render { display: flex; flex-direction: column; }
.widget-title {
  font-size: 14px;
  font-weight: 600;
  padding: 2px 4px;
  color: inherit;
}
.widget-text { flex: 1; display: flex; align-items: center; justify-content: center; }
.widget-image { width: 100%; height: 100%; object-fit: contain; }
.widget-chart { flex: 1; min-height: 0; }
.widget-table-wrap { flex: 1; overflow: auto; }
.widget-table {
  width: 100%; border-collapse: collapse; color: inherit; font-size: 12px;
}
.widget-table th, .widget-table td {
  border: 1px solid rgba(255,255,255,0.15); padding: 4px 8px; text-align: left;
}
.widget-table th { background: rgba(255,255,255,0.08); }
</style>
