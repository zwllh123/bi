import { post, postPage } from './request'
import type { SqlExecuteResult } from './datasource'

export interface DataSet {
  id: number
  setCode: string
  setName: string
  setDesc?: string
  sourceCode: string
  dynSentence?: string
  caseResult?: string
  setType: string
  enableFlag: number
}

export interface DataSetParam {
  id: number
  setCode: string
  paramName: string
  paramDesc?: string
  paramType?: string
  sampleItem?: string
  requiredFlag: number
  validationRules?: string
  orderNum: number
}

export interface DataSetTransform {
  id: number
  setCode: string
  transformType: string
  transformScript?: string
  orderNum: number
  enableFlag: number
}

export const setListByPage = (data: { index: number; size: number; keywords?: string; sourceCode?: string; enableFlag?: number }) =>
  postPage<DataSet>('/dataSet/listByPage', data)

export const setInsert = (data: Partial<DataSet>) => post('/dataSet/insertSet', data)
export const setUpdate = (data: Partial<DataSet>) => post('/dataSet/updateSet', data)
export const setDelete = (id: number) => post('/dataSet/deleteSet', { id })

export interface DataSetDetail {
  dataSet: DataSet
  params: DataSetParam[]
  transforms: DataSetTransform[]
}

export const setDetail = (id: number) => post<DataSetDetail>('/dataSet/detail', { id })

export const setExecute = (data: { setCode: string; parameters?: Record<string, unknown>; maxRows?: number }) =>
  post<SqlExecuteResult>('/dataSet/execute', data)

export const paramListBySet = (setCode: string) => post<DataSetParam[]>('/dataSet/param/listBySet', { code: setCode })
export const paramInsert = (data: Partial<DataSetParam>) => post('/dataSet/param/insert', data)
export const paramUpdate = (data: Partial<DataSetParam>) => post('/dataSet/param/update', data)
export const paramDelete = (id: number) => post('/dataSet/param/delete', { id })

export const trListBySet = (setCode: string) => post<DataSetTransform[]>('/dataSet/transform/listBySet', { code: setCode })
export const trInsert = (data: Partial<DataSetTransform>) => post('/dataSet/transform/insert', data)
export const trUpdate = (data: Partial<DataSetTransform>) => post('/dataSet/transform/update', data)
export const trDelete = (id: number) => post('/dataSet/transform/delete', { id })
