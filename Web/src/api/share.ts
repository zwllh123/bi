import { post } from './request'

export interface ReportShare {
  id: number
  shareCode: string
  shareValidType: number
  shareValidTime: string
  shareToken: string
  shareUrl: string
  sharePassword?: string
  reportCode: string
  enableFlag: number
}

export const shareCreate = (data: { reportCode: string; shareValidType: number; shareValidTime?: string; sharePassword?: string }) =>
  post<ReportShare>('/share/create', data)

export const shareListByReport = (reportCode: string) => post<ReportShare[]>('/share/listByReport', { reportCode })

export const shareDelete = (id: number) => post('/share/delete', { id })

export const shareVerify = (data: { shareCode: string; password?: string }) =>
  post<{ reportCode?: string; token?: string; needPassword: boolean }>('/share/verify', data)
