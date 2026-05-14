// 部件类型与默认配置
export interface WidgetPosition {
  x: number
  y: number
  width: number
  height: number
}

export interface WidgetSetup {
  title?: string
  showTitle?: boolean
  colors?: string[]
  bgColor?: string
  textColor?: string
  fontSize?: number
  content?: string
  url?: string
  refreshSeconds?: number
}

export interface WidgetDataConfig {
  mode: 'static' | 'dataset'
  setCode?: string
  staticData?: Array<Record<string, unknown>>
  xField?: string
  yField?: string
  nameField?: string
  valueField?: string
  parameters?: Record<string, unknown>
}

export interface WidgetModel {
  id?: number
  tempId: string
  reportCode: string
  type: string
  position: WidgetPosition
  setup: WidgetSetup
  data: WidgetDataConfig
  sort: number
}

export interface WidgetDefinition {
  type: string
  name: string
  icon: string
  defaultPosition: WidgetPosition
  defaultSetup: WidgetSetup
  defaultData: WidgetDataConfig
}

const DEFAULT_COLORS = ['#5470c6', '#91cc75', '#fac858', '#ee6666', '#73c0de', '#3ba272', '#fc8452', '#9a60b4']

export const WIDGET_LIST: WidgetDefinition[] = [
  {
    type: 'text',
    name: '文本',
    icon: 'IconEditPen',
    defaultPosition: { x: 40, y: 40, width: 200, height: 60 },
    defaultSetup: { content: '文本内容', fontSize: 18, textColor: '#ffffff', bgColor: 'transparent' },
    defaultData: { mode: 'static' },
  },
  {
    type: 'image',
    name: '图片',
    icon: 'IconPicture',
    defaultPosition: { x: 40, y: 40, width: 240, height: 160 },
    defaultSetup: { url: '' },
    defaultData: { mode: 'static' },
  },
  {
    type: 'line',
    name: '折线图',
    icon: 'IconTrendCharts',
    defaultPosition: { x: 60, y: 60, width: 480, height: 280 },
    defaultSetup: { title: '折线图', showTitle: true, colors: DEFAULT_COLORS, bgColor: 'rgba(255,255,255,0.04)' },
    defaultData: { mode: 'static', staticData: [
      { name: '1月', value: 120 }, { name: '2月', value: 200 },
      { name: '3月', value: 150 }, { name: '4月', value: 80 },
      { name: '5月', value: 70 }, { name: '6月', value: 110 },
    ], xField: 'name', yField: 'value' },
  },
  {
    type: 'bar',
    name: '柱状图',
    icon: 'IconHistogram',
    defaultPosition: { x: 60, y: 60, width: 480, height: 280 },
    defaultSetup: { title: '柱状图', showTitle: true, colors: DEFAULT_COLORS, bgColor: 'rgba(255,255,255,0.04)' },
    defaultData: { mode: 'static', staticData: [
      { name: 'A', value: 120 }, { name: 'B', value: 200 },
      { name: 'C', value: 150 }, { name: 'D', value: 80 },
    ], xField: 'name', yField: 'value' },
  },
  {
    type: 'pie',
    name: '饼图',
    icon: 'IconPieChart',
    defaultPosition: { x: 60, y: 60, width: 360, height: 280 },
    defaultSetup: { title: '饼图', showTitle: true, colors: DEFAULT_COLORS, bgColor: 'rgba(255,255,255,0.04)' },
    defaultData: { mode: 'static', staticData: [
      { name: '类别 A', value: 335 }, { name: '类别 B', value: 310 },
      { name: '类别 C', value: 234 }, { name: '类别 D', value: 135 },
    ], nameField: 'name', valueField: 'value' },
  },
  {
    type: 'table',
    name: '表格',
    icon: 'IconGrid',
    defaultPosition: { x: 60, y: 60, width: 520, height: 280 },
    defaultSetup: { title: '数据表格', showTitle: true, bgColor: 'rgba(255,255,255,0.04)', textColor: '#ffffff' },
    defaultData: { mode: 'static', staticData: [
      { name: '示例 1', value: 100 }, { name: '示例 2', value: 200 },
    ] },
  },
]

export function genTempId(): string {
  return 'w_' + Date.now().toString(36) + Math.random().toString(36).slice(2, 6)
}

export function widgetFromDef(def: WidgetDefinition, reportCode: string, sort: number): WidgetModel {
  return {
    tempId: genTempId(),
    reportCode,
    type: def.type,
    position: { ...def.defaultPosition },
    setup: JSON.parse(JSON.stringify(def.defaultSetup)),
    data: JSON.parse(JSON.stringify(def.defaultData)),
    sort,
  }
}
