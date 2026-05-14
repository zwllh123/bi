import { post, postPage } from './request'

export interface DataSource {
  id: number
  sourceCode: string
  sourceName: string
  sourceDesc?: string
  sourceType: string
  sourceConfig?: string
  enableFlag: number
}

export const dsListByPage = (data: { index: number; size: number; keywords?: string; enableFlag?: number }) =>
  postPage<DataSource>('/dataSource/listByPage', data)

export const dsListAll = () => post<DataSource[]>('/dataSource/listAll')

export const dsInsert = (data: Partial<DataSource>) => post('/dataSource/insertSource', data)

export const dsUpdate = (data: Partial<DataSource>) => post('/dataSource/updateSource', data)

export const dsDelete = (id: number) => post('/dataSource/deleteSource', { id })

export const dsTestConnection = (data: { id?: number; sourceCode?: string; sourceType?: string; sourceConfig?: string }) =>
  post('/dataSource/testConnection', data)

export interface SqlExecuteResult {
  columns: string[]
  rows: Record<string, unknown>[]
  total: number
}

export const dsExecuteSql = (data: { sourceCode: string; sql: string; parameters?: Record<string, unknown>; maxRows?: number }) =>
  post<SqlExecuteResult>('/dataSource/executeSql', data)
