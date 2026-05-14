<template>
  <div class="preview-root" :style="rootStyle">
    <div class="preview-canvas" :style="canvasStyle">
      <div v-for="w in widgets" :key="w.tempId" class="widget-box" :style="boxStyle(w)">
        <WidgetRender :widget="w" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onBeforeUnmount } from 'vue'
import { useRoute } from 'vue-router'
import { dashLoad } from '@/api/report'
import WidgetRender from '@/views/designer/WidgetRender.vue'
import type { WidgetModel } from '@/views/designer/widgets'

const route = useRoute()
const reportCode = String(route.params.reportCode)
const widgets = ref<WidgetModel[]>([])
const dashboard = ref({ width: 1920, height: 1080, backgroundColor: '#1e1e2f', refreshSeconds: 0 })

const rootStyle = computed(() => ({
  background: dashboard.value.backgroundColor || '#1e1e2f',
  width: '100vw', height: '100vh', overflow: 'auto',
}))
const canvasStyle = computed(() => ({
  width: dashboard.value.width + 'px',
  height: dashboard.value.height + 'px',
  position: 'relative' as const,
  margin: '0 auto',
}))

function boxStyle(w: WidgetModel) {
  return {
    position: 'absolute' as const,
    left: w.position.x + 'px',
    top: w.position.y + 'px',
    width: w.position.width + 'px',
    height: w.position.height + 'px',
  }
}

let timer: ReturnType<typeof setInterval> | null = null

async function load() {
  const detail = await dashLoad(reportCode)
  if (detail?.dashboard) {
    dashboard.value = {
      width: detail.dashboard.width || 1920,
      height: detail.dashboard.height || 1080,
      backgroundColor: detail.dashboard.backgroundColor || '#1e1e2f',
      refreshSeconds: detail.dashboard.refreshSeconds || 0,
    }
  }
  widgets.value = (detail?.widgets || []).map((w, i) => {
    let pos = { x: 0, y: 0, width: 320, height: 240 }
    let setup: WidgetModel['setup'] = {}
    let data: WidgetModel['data'] = { mode: 'static' }
    try { if (w.position) pos = { ...pos, ...JSON.parse(w.position) } } catch { /* ignore */ }
    try { if (w.setup) setup = JSON.parse(w.setup) } catch { /* ignore */ }
    try { if (w.data) data = JSON.parse(w.data) } catch { /* ignore */ }
    return { id: w.id, tempId: 'w_' + w.id, reportCode, type: w.type, position: pos, setup, data, sort: w.sort ?? i }
  })
}

onMounted(async () => {
  await load()
  if (dashboard.value.refreshSeconds && dashboard.value.refreshSeconds > 0) {
    timer = setInterval(load, dashboard.value.refreshSeconds * 1000)
  }
})
onBeforeUnmount(() => { if (timer) clearInterval(timer) })
</script>
