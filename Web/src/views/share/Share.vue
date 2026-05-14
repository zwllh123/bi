<template>
  <div class="share-root">
    <div v-if="needPwd" class="pwd-box">
      <h3>请输入访问密码</h3>
      <el-input v-model="password" type="password" placeholder="访问密码" show-password />
      <el-button type="primary" style="margin-top:12px;width:100%" @click="onVerify">进入</el-button>
    </div>
    <Preview v-else-if="ok" />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { shareVerify } from '@/api/share'
import Preview from '@/views/preview/Preview.vue'

const route = useRoute()
const router = useRouter()
const shareCode = String(route.params.shareCode)
const password = ref('')
const needPwd = ref(false)
const ok = ref(false)

async function tryVerify(pwd?: string) {
  const r = await shareVerify({ shareCode, password: pwd })
  if (r.needPassword) {
    needPwd.value = true
    return
  }
  if (r.reportCode) {
    needPwd.value = false
    ok.value = true
    // 复用 Preview 组件，跳转到对应 reportCode 路由
    router.replace(`/preview/${r.reportCode}`)
  } else {
    ElMessage.error('分享链接无效')
  }
}

function onVerify() {
  if (!password.value) { ElMessage.warning('请输入密码'); return }
  tryVerify(password.value)
}

onMounted(() => tryVerify())
</script>

<style scoped>
.share-root {
  width: 100vw; height: 100vh;
  display: flex; align-items: center; justify-content: center;
  background: #1e1e2f;
}
.pwd-box {
  background: #fff; padding: 24px; border-radius: 8px; width: 320px;
}
.pwd-box h3 { margin: 0 0 16px; text-align: center; }
</style>
