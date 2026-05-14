<template>
  <div class="page-card">
    <div class="toolbar">
      <el-input v-model="query.keywords" placeholder="关键字" clearable style="width: 200px" />
      <el-select v-model="query.reportType" placeholder="类型" clearable style="width: 140px">
        <el-option label="大屏报表" value="report_screen" />
        <el-option label="Excel报表" value="report_excel" />
      </el-select>
      <el-button type="primary" @click="load(1)">查询</el-button>
      <div class="grow"></div>
      <el-button type="primary" @click="onAdd">新建报表</el-button>
    </div>
    <el-table :data="list" border>
      <el-table-column prop="id" label="Id" width="80" />
      <el-table-column prop="reportCode" label="编码" />
      <el-table-column prop="reportName" label="名称" />
      <el-table-column prop="reportType" label="类型" width="120">
        <template #default="{ row }">{{ row.reportType === 'report_screen' ? '大屏' : 'Excel' }}</template>
      </el-table-column>
      <el-table-column prop="reportGroup" label="分组" width="120" />
      <el-table-column prop="reportAuthor" label="作者" width="120" />
      <el-table-column label="操作" width="280">
        <template #default="{ row }">
          <el-button size="small" type="primary" @click="onDesign(row)">设计</el-button>
          <el-button size="small" @click="onPreview(row)">预览</el-button>
          <el-button size="small" @click="onEdit(row)">编辑</el-button>
          <el-button size="small" type="danger" @click="onDelete(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination class="pager" v-model:current-page="query.index" v-model:page-size="query.size"
      :total="total" layout="total, sizes, prev, pager, next, jumper" @change="() => load()" />

    <el-dialog v-model="editVisible" :title="editForm.id ? '编辑报表' : '新建报表'" width="520px">
      <el-form :model="editForm" label-width="80px">
        <el-form-item label="编码"><el-input v-model="editForm.reportCode" :disabled="!!editForm.id" /></el-form-item>
        <el-form-item label="名称"><el-input v-model="editForm.reportName" /></el-form-item>
        <el-form-item label="类型">
          <el-select v-model="editForm.reportType">
            <el-option label="大屏报表" value="report_screen" />
            <el-option label="Excel报表" value="report_excel" />
          </el-select>
        </el-form-item>
        <el-form-item label="分组"><el-input v-model="editForm.reportGroup" /></el-form-item>
        <el-form-item label="作者"><el-input v-model="editForm.reportAuthor" /></el-form-item>
        <el-form-item label="描述"><el-input v-model="editForm.reportDesc" type="textarea" /></el-form-item>
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
import { reportListByPage, reportInsert, reportUpdate, reportDelete, type Report } from '@/api/report'

const router = useRouter()
const query = reactive({ index: 1, size: 20, keywords: '', reportType: '' })
const list = ref<Report[]>([])
const total = ref(0)
const editVisible = ref(false)
const editForm = ref<Partial<Report>>({ reportType: 'report_screen', enableFlag: 1 })

async function load(idx?: number) {
  if (idx) query.index = idx
  const r = await reportListByPage({ ...query })
  list.value = r.list
  total.value = r.total
}
onMounted(load)

function onAdd() {
  editForm.value = { reportType: 'report_screen', enableFlag: 1 }
  editVisible.value = true
}
function onEdit(row: Report) {
  editForm.value = { ...row }
  editVisible.value = true
}
async function onSave() {
  if (!editForm.value.reportCode || !editForm.value.reportName) {
    ElMessage.warning('编码与名称必填')
    return
  }
  if (editForm.value.id) await reportUpdate(editForm.value)
  else await reportInsert(editForm.value)
  ElMessage.success('保存成功')
  editVisible.value = false
  load()
}
async function onDelete(row: Report) {
  await ElMessageBox.confirm(`确认删除 ${row.reportName}？`, '提示', { type: 'warning' })
  await reportDelete(row.id)
  ElMessage.success('删除成功')
  load()
}
function onDesign(row: Report) {
  if (row.reportType === 'report_screen') router.push(`/designer/${row.reportCode}`)
  else ElMessage.info('Excel 报表设计器待实现')
}
function onPreview(row: Report) {
  window.open(`/preview/${row.reportCode}`, '_blank')
}
</script>

<style scoped>
.pager { margin-top: 12px; justify-content: flex-end; display: flex; }
</style>
