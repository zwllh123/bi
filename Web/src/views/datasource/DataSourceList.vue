<template>
  <div class="page-card">
    <div class="toolbar">
      <el-input v-model="query.keywords" placeholder="关键字" clearable style="width: 200px" />
      <el-button type="primary" @click="load(1)">查询</el-button>
      <div class="grow"></div>
      <el-button type="primary" @click="onAdd">新建数据源</el-button>
    </div>
    <el-table :data="list" border>
      <el-table-column prop="id" label="Id" width="80" />
      <el-table-column prop="sourceCode" label="编码" />
      <el-table-column prop="sourceName" label="名称" />
      <el-table-column prop="sourceType" label="类型" width="120" />
      <el-table-column prop="enableFlag" label="启用" width="80">
        <template #default="{ row }">{{ row.enableFlag === 1 ? '是' : '否' }}</template>
      </el-table-column>
      <el-table-column label="操作" width="260">
        <template #default="{ row }">
          <el-button size="small" @click="onTest(row)">测试</el-button>
          <el-button size="small" @click="onEdit(row)">编辑</el-button>
          <el-button size="small" type="danger" @click="onDelete(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination class="pager" v-model:current-page="query.index" v-model:page-size="query.size"
      :total="total" layout="total, sizes, prev, pager, next, jumper" @change="() => load()" />

    <el-dialog v-model="editVisible" :title="editForm.id ? '编辑数据源' : '新建数据源'" width="560px">
      <el-form :model="editForm" label-width="100px">
        <el-form-item label="编码"><el-input v-model="editForm.sourceCode" :disabled="!!editForm.id" /></el-form-item>
        <el-form-item label="名称"><el-input v-model="editForm.sourceName" /></el-form-item>
        <el-form-item label="类型">
          <el-select v-model="editForm.sourceType">
            <el-option label="MySQL" value="mysql" />
            <el-option label="SQL Server" value="sqlserver" />
            <el-option label="PostgreSQL" value="postgresql" />
            <el-option label="Oracle" value="oracle" />
            <el-option label="SQLite" value="sqlite" />
          </el-select>
        </el-form-item>
        <el-form-item label="连接配置">
          <el-input v-model="editForm.sourceConfig" type="textarea" :rows="6"
            placeholder='{"connectionString":"server=...;uid=...;pwd=...;database=..."}' />
        </el-form-item>
        <el-form-item label="描述"><el-input v-model="editForm.sourceDesc" type="textarea" /></el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="onTest(editForm)">测试连接</el-button>
        <el-button @click="editVisible = false">取消</el-button>
        <el-button type="primary" @click="onSave">保存</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { dsListByPage, dsInsert, dsUpdate, dsDelete, dsTestConnection, type DataSource } from '@/api/datasource'

const query = reactive({ index: 1, size: 20, keywords: '' })
const list = ref<DataSource[]>([])
const total = ref(0)
const editVisible = ref(false)
const editForm = ref<Partial<DataSource>>({ sourceType: 'mysql', enableFlag: 1 })

async function load(idx?: number) {
  if (idx) query.index = idx
  const r = await dsListByPage({ ...query })
  list.value = r.list
  total.value = r.total
}
onMounted(load)

function onAdd() { editForm.value = { sourceType: 'mysql', enableFlag: 1 }; editVisible.value = true }
function onEdit(row: DataSource) { editForm.value = { ...row }; editVisible.value = true }

async function onSave() {
  if (!editForm.value.sourceCode || !editForm.value.sourceName) {
    ElMessage.warning('编码与名称必填'); return
  }
  if (editForm.value.id) await dsUpdate(editForm.value)
  else await dsInsert(editForm.value)
  ElMessage.success('保存成功')
  editVisible.value = false
  load()
}

async function onDelete(row: DataSource) {
  await ElMessageBox.confirm(`确认删除 ${row.sourceName}？`, '提示', { type: 'warning' })
  await dsDelete(row.id)
  ElMessage.success('删除成功')
  load()
}

async function onTest(row: Partial<DataSource>) {
  try {
    await dsTestConnection({ id: row.id, sourceCode: row.sourceCode, sourceType: row.sourceType, sourceConfig: row.sourceConfig })
    ElMessage.success('连接成功')
  } catch {
    // 错误信息由拦截器提示
  }
}
</script>

<style scoped>
.pager { margin-top: 12px; justify-content: flex-end; display: flex; }
</style>
