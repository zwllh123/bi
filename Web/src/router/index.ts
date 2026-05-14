import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import { useUserStore } from '@/stores/user'

const routes: RouteRecordRaw[] = [
  { path: '/login', component: () => import('@/views/Login.vue'), meta: { public: true } },
  { path: '/share/:shareCode', component: () => import('@/views/share/Share.vue'), meta: { public: true } },
  { path: '/preview/:reportCode', component: () => import('@/views/preview/Preview.vue'), meta: { public: true } },
  {
    path: '/',
    component: () => import('@/layout/MainLayout.vue'),
    redirect: '/home',
    children: [
      { path: 'home', component: () => import('@/views/Home.vue'), meta: { title: '首页' } },
      { path: 'report', component: () => import('@/views/report/ReportList.vue'), meta: { title: '报表管理' } },
      { path: 'designer/:reportCode', component: () => import('@/views/designer/Designer.vue'), meta: { title: '大屏设计器' } },
      { path: 'datasource', component: () => import('@/views/datasource/DataSourceList.vue'), meta: { title: '数据源' } },
      { path: 'dataset', component: () => import('@/views/dataset/DataSetList.vue'), meta: { title: '数据集' } },
      { path: 'dataset/:setCode/edit', component: () => import('@/views/dataset/DataSetEdit.vue'), meta: { title: '数据集详情' } },
      { path: 'dict', component: () => import('@/views/dict/DictList.vue'), meta: { title: '数据字典' } },
      { path: 'access/user', component: () => import('@/views/access/UserList.vue'), meta: { title: '用户管理' } },
      { path: 'access/role', component: () => import('@/views/access/RoleList.vue'), meta: { title: '角色管理' } },
    ],
  },
  { path: '/:pathMatch(.*)*', redirect: '/home' },
]

const router = createRouter({ history: createWebHistory(import.meta.env.BASE_URL), routes })

router.beforeEach((to, _from, next) => {
  const store = useUserStore()
  if (to.meta.public) return next()
  if (!store.isLogin) return next('/login')
  next()
})

export default router
