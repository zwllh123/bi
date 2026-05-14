import { post, postPage } from './request'

export interface Dict {
  id: number
  dictCode: string
  dictName: string
  remark?: string
}

export interface DictItem {
  id: number
  dictCode: string
  itemName: string
  itemValue: string
  itemExtend?: string
  enabled: number
  locale?: string
  remark?: string
  sort: number
}

export const dictListByPage = (data: { index: number; size: number; keywords?: string }) =>
  postPage<Dict>('/dict/listByPage', data)
export const dictInsert = (data: Partial<Dict>) => post('/dict/insertDict', data)
export const dictUpdate = (data: Partial<Dict>) => post('/dict/updateDict', data)
export const dictDelete = (id: number) => post('/dict/deleteDict', { id })

export const dictItemListByPage = (data: { index: number; size: number; keywords?: string; dictCode?: string; enabled?: number }) =>
  postPage<DictItem>('/dict/item/listByPage', data)
export const dictItemByCode = (dictCode: string) => post<DictItem[]>('/dict/item/listByDictCode', { dictCode })
export const dictItemInsert = (data: Partial<DictItem>) => post('/dict/item/insertItem', data)
export const dictItemUpdate = (data: Partial<DictItem>) => post('/dict/item/updateItem', data)
export const dictItemDelete = (id: number) => post('/dict/item/deleteItem', { id })
