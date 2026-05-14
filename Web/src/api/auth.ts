import { post } from './request'

export interface LoginResponse {
  token: string
  loginName: string
  realName: string
  roles: string[]
}

export const login = (loginName: string, password: string) =>
  post<LoginResponse>('/auth/login', { loginName, password })

export const logout = () => post<void>('/auth/logout')

export const me = () => post<LoginResponse>('/auth/me')
