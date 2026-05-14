<template>
  <div class="login-root">
    <div class="login-box">
      <h2>🫒 橄榄云 BI</h2>
      <el-form @submit.prevent="onLogin">
        <el-form-item>
          <el-input v-model="form.loginName" placeholder="登录名" size="large" :prefix-icon="IconUser" />
        </el-form-item>
        <el-form-item>
          <el-input v-model="form.password" type="password" placeholder="密码" size="large" :prefix-icon="IconLock" show-password />
        </el-form-item>
        <el-button type="primary" :loading="loading" @click="onLogin" size="large" style="width: 100%">登 录</el-button>
      </el-form>
      <p class="tip">默认账号 admin / 123456</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, shallowRef } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { useUserStore } from '@/stores/user'
import { User as IconUser, Lock as IconLock } from '@element-plus/icons-vue'

const router = useRouter()
const store = useUserStore()
const form = reactive({ loginName: 'admin', password: '123456' })
const loading = ref(false)
shallowRef(IconUser); shallowRef(IconLock)

async function onLogin() {
  if (!form.loginName || !form.password) {
    ElMessage.warning('请输入账号密码')
    return
  }
  loading.value = true
  try {
    await store.login(form.loginName, form.password)
    router.push('/home')
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.login-root {
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}
.login-box {
  width: 360px;
  background: #fff;
  padding: 32px;
  border-radius: 8px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
}
.login-box h2 {
  text-align: center;
  margin: 0 0 24px;
  color: #303133;
}
.tip {
  text-align: center;
  color: #909399;
  font-size: 12px;
  margin-top: 12px;
}
</style>
