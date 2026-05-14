import { defineStore } from 'pinia'
import { login as apiLogin, logout as apiLogout } from '@/api/auth'

export const useUserStore = defineStore('user', {
  state: () => ({
    token: localStorage.getItem('olive_token') || '',
    loginName: localStorage.getItem('olive_loginName') || '',
    realName: localStorage.getItem('olive_realName') || '',
    roles: JSON.parse(localStorage.getItem('olive_roles') || '[]') as string[],
  }),
  getters: {
    isLogin: (s) => !!s.token,
  },
  actions: {
    async login(loginName: string, password: string) {
      const r = await apiLogin(loginName, password)
      this.token = r.token
      this.loginName = r.loginName
      this.realName = r.realName
      this.roles = r.roles || []
      localStorage.setItem('olive_token', this.token)
      localStorage.setItem('olive_loginName', this.loginName)
      localStorage.setItem('olive_realName', this.realName)
      localStorage.setItem('olive_roles', JSON.stringify(this.roles))
    },
    async logout() {
      try {
        if (this.token) await apiLogout()
      } catch {
        // ignore
      }
      this.token = ''
      this.loginName = ''
      this.realName = ''
      this.roles = []
      localStorage.removeItem('olive_token')
      localStorage.removeItem('olive_loginName')
      localStorage.removeItem('olive_realName')
      localStorage.removeItem('olive_roles')
    },
  },
})
