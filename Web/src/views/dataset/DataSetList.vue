<template>
  <div class="page-card">
    <div class="toolbar">
      <el-input v-model="query.keywords" placeholder="关键字" clearable style="width: 200px" />
      <el-button type="primary" @click="load(1)">查询</el-button>
      <div class="grow"></div>
      <el-button type="primary" @click="onAdd">新建数据集</el-button>
    </div>
    <el-table :data="list" border>
      <el-table-column prop="id" label="Id" width="80" />
      <el-table-column prop="setCode" label="编码" />
      <el-table-column prop="setName" label="名称" />
      <el-table-column prop="sourceCode" label="数据源" width="160" />
      <el-table-column prop="setType" label="类型" width="100" />
      <el-table-column label="操作" width="280">
        <template #default="{ row }">
          <el-button size="small" type="primary" @click="onEditDetail(row)">详情</el-button>
          <el-button size="small" @click="onEdit(row)">编辑</el-button>
          <el-button size="small" type="danger" @click="onDelete(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination class="pager" v-model:current-page="query.index" v-model:page-size="query.size"
      :total="total" layout="total, sizes, prev, pager, next, jumper" @change="() => load()" />

    <el-dialog v-model="editVisible" :title="editForm.id ? '编辑数据集' : '新建数据集'" width="600px">
      <el-form :model="editForm" label-width="100px">
        <el-form-item label="编码"><el-input v-model="editForm.setCode" :disabled="!!editForm.id" /></el-form-item>
        <el-form-item label="名称"><el-input v-model="editForm.setName" /></el-form-item>
        <el-form-item label="数据源">
          <el-select v-model="editForm.sourceCode" filterable>
            <el-option v-for="s in sources" :key="s.sourceCode" :label="s.sourceName" :value="s.sourceCode" />
          </el-select>
        </el-form-item>
        <el-form-item label="类型">
          <el-select v-model="editForm.setType">
            <el-option label="SQL" value="sql" />
            <el-option label="HTTP" value="http" />
          </el-select>
        </el-form-item>
        <el-form-item label="SQL/请求体"><el-input v-model="editForm.dynSentence" type="textarea" :rows="6" /></el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="editVisible = false">取消</el-button>
        <el-button type="primary" @click="onSave">保存</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { useRouter } from 'vue-router'
import { setListByPage, setInsert, setUpdate, setDelete, type DataSet } from '@/api/dataset'
import { dsListAll, type DataSource } from '@/api/datasource'

const router = useRouter()
const query = reactive({ index: 1, size: 20, keywords: '' })
const list = ref<DataSet[]>([])
const total = ref(0)
const editVisible = ref(false)
const editForm = ref<Partial<DataSet>>({ setType: 'sql', enableFlag: 1 })
const sources = ref<DataSource[]>([])

async function load(idx?: number) {
  if (idx) query.index = idx
  const r = await setListByPage({ ...query })
  list.value = r.list
  total.value = r.total
}
onMounted(async () => {
  sources.value = (await dsListAll()) || []
  load()
})

function onAdd() { editForm.value = { setType: 'sql', enableFlag: 1 }; editVisible.value = true }
function onEdit(row: DataSet) { editForm.value = { ...row }; editVisible.value = true }
function onEditDetail(row: DataSet) { router.push(`/dataset/${row.setCode}/edit`) }

async function onSave() {
  if (!editForm.value.setCode || !editForm.value.setName) { ElMessage.warning('编码与名称必填'); return }
  if (editForm.value.id) await setUpdate(editForm.value)
  else await setInsert(editForm.value)
  ElMessage.success('保存成功')
  editVisible.value = false
  load()
}

async function onDelete(row: DataSet) {
  await ElMessageBox.confirm(`确认删除 ${row.setName}？`, '提示', { type: 'warning' })
  await setDelete(row.id)
  ElMessage.success('删除成功')
  load()
}
</script>

<style scoped>
.pager { margin-top: 12px; justify-content: flex-end; display: flex; }
</style>
