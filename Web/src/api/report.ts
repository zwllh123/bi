import { post, postPage } from './request'

export interface Report {
  id: number
  reportCode: string
  reportName: string
  reportGroup?: string
  reportType: string
  reportImage?: string
  reportDesc?: string
  reportAuthor?: string
  downloadCount?: number
  enableFlag: number
}

export const reportListByPage = (data: { index: number; size: number; keywords?: string; reportType?: string; reportGroup?: string; enableFlag?: number }) =>
  postPage<Report>('/report/listByPage', data)

export const reportInsert = (data: Partial<Report>) => post<Report>('/report/insertReport', data)
export const reportUpdate = (data: Partial<Report>) => post('/report/updateReport', data)
export const reportDelete = (id: number) => post('/report/deleteReport', { id })
export const reportGetByCode = (code: string) => post<Report>('/report/getByCode', { code })

export interface Dashboard {
  id: number
  reportCode: string
  title?: string
  width: number
  height: number
  backgroundColor?: string
  backgroundImage?: string
  presetLine?: string
  refreshSeconds?: number
  enableFlag: number
  sort: number
}

export interface DashboardWidget {
  id: number
  reportCode: string
  type: string
  setup?: string
  data?: string
  collapse?: string
  position?: string
  options?: string
  refreshSeconds?: number
  enableFlag: number
  sort: number
}

export interface DashboardDetail {
  dashboard: Dashboard
  widgets: DashboardWidget[]
}

export const dashLoad = (reportCode: string) => post<DashboardDetail>('/dashboard/load', { reportCode })
export const dashSave = (data: Partial<Dashboard>) => post('/dashboard/saveDashboard', data)
export const dashSaveWidgets = (data: { reportCode: string; widgets: Partial<DashboardWidget>[] }) =>
  post('/dashboard/saveWidgets', data)
export const dashSaveWidget = (data: Partial<DashboardWidget>) => post<number>('/dashboard/widget/save', data)
export const dashDeleteWidget = (id: number) => post('/dashboard/widget/delete', { id })
