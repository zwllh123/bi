import { post } from './request'
import instance from './request'

export interface UploadResponse {
  fileId: string
  urlPath?: string
  fileType?: string
}

export const upload = async (file: File, instruction?: string): Promise<UploadResponse> => {
  const fd = new FormData()
  fd.append('file', file)
  if (instruction) fd.append('instruction', instruction)
  const r = await instance.post('/file/upload', fd, { headers: { 'Content-Type': 'multipart/form-data' } })
  return r.data.content
}

export const fileDelete = (id: number) => post('/file/delete', { id })
