<template>
  <el-container class="layout-root">
    <el-aside width="220px" class="sidebar">
      <div class="logo">🫒 橄榄云 BI</div>
      <el-menu :default-active="$route.path" router background-color="#1f2937" text-color="#cbd5e1" active-text-color="#22d3ee">
        <el-menu-item index="/home"><el-icon><IconHomeFilled /></el-icon><span>首页</span></el-menu-item>
        <el-menu-item index="/report"><el-icon><IconDataAnalysis /></el-icon><span>报表管理</span></el-menu-item>
        <el-menu-item index="/dataset"><el-icon><IconCollection /></el-icon><span>数据集</span></el-menu-item>
        <el-menu-item index="/datasource"><el-icon><IconCoin /></el-icon><span>数据源</span></el-menu-item>
        <el-sub-menu index="access">
          <template #title><el-icon><IconUser /></el-icon><span>权限管理</span></template>
          <el-menu-item index="/access/user">用户</el-menu-item>
          <el-menu-item index="/access/role">角色</el-menu-item>
        </el-sub-menu>
        <el-menu-item index="/dict"><el-icon><IconReading /></el-icon><span>数据字典</span></el-menu-item>
      </el-menu>
    </el-aside>
    <el-container>
      <el-header class="header">
        <div class="header-title">{{ ($route.meta.title as string) || '' }}</div>
        <div class="grow"></div>
        <el-dropdown @command="onCommand">
          <span class="user-info">
            <el-icon><IconUser /></el-icon> {{ store.realName || store.loginName }}
          </span>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="logout">退出登录</el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-header>
      <el-main class="main">
        <router-view />
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import { useUserStore } from '@/stores/user'
import { useRouter } from 'vue-router'

const store = useUserStore()
const router = useRouter()

async function onCommand(cmd: string) {
  if (cmd === 'logout') {
    await store.logout()
    router.push('/login')
  }
}
</script>

<style scoped>
.layout-root { height: 100vh; }
.sidebar {
  background: #1f2937;
  color: #cbd5e1;
}
.logo {
  height: 56px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 18px;
  font-weight: 600;
  color: #22d3ee;
  border-bottom: 1px solid #374151;
}
.sidebar :deep(.el-menu) { border-right: none; }
.header {
  background: #fff;
  border-bottom: 1px solid #ebeef5;
  display: flex;
  align-items: center;
  padding: 0 16px;
}
.header-title { font-size: 16px; font-weight: 600; }
.grow { flex: 1; }
.user-info { cursor: pointer; display: inline-flex; align-items: center; gap: 4px; }
.main { padding: 16px; background: #f5f7fa; }
</style>
