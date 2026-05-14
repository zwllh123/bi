import axios, { AxiosInstance, AxiosRequestConfig } from 'axios'
import { ElMessage } from 'element-plus'
import { useUserStore } from '@/stores/user'

const instance: AxiosInstance = axios.create({
  baseURL: '/',
  timeout: 30000,
})

instance.interceptors.request.use((config) => {
  const store = useUserStore()
  if (store.token) {
    config.headers = config.headers || {}
    config.headers.Authorization = `Bearer ${store.token}`
  }
  return config
})

instance.interceptors.response.use(
  (resp) => {
    const data = resp.data
    // 后端统一返回 { state, msg, content }
    if (data && typeof data === 'object' && 'state' in data) {
      if (data.state !== 0) {
        ElMessage.error(data.msg || '请求失败')
        return Promise.reject(data)
      }
    }
    return resp
  },
  (err) => {
    if (err.response?.status === 401) {
      const store = useUserStore()
      store.logout()
      ElMessage.error('登录已失效，请重新登录')
      window.location.href = '/login'
    } else {
      ElMessage.error(err.response?.data?.msg || err.message || '网络错误')
    }
    return Promise.reject(err)
  }
)

export interface ApiResult<T = unknown> {
  state: number
  msg?: string
  content?: T
}

export interface ApiResults<T = unknown> {
  state: number
  msg?: string
  content?: {
    list: T[]
    total: number
  }
}

export async function post<T = unknown>(url: string, data?: unknown, config?: AxiosRequestConfig): Promise<T> {
  const r = await instance.post<ApiResult<T>>(url, data, config)
  return r.data.content as T
}

export async function postPage<T = unknown>(url: string, data?: unknown): Promise<{ list: T[]; total: number }> {
  const r = await instance.post<ApiResults<T>>(url, data)
  return r.data.content || { list: [], total: 0 }
}

export async function get<T = unknown>(url: string, config?: AxiosRequestConfig): Promise<T> {
  const r = await instance.get<ApiResult<T>>(url, config)
  return r.data.content as T
}

export default instance
