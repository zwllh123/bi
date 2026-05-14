# 橄榄云 BI - 前端

基于 **Vue 3 + TypeScript + Vite + Element Plus + ECharts** 的 BI 报表前端，对接后端 `Olive.BI.Web`（端口 5000）。

设计风格延续 AJ-Report 经典三栏布局：**左工具栏 + 中央画布 + 右属性面板**。

## 启动

```bash
cd Web
pnpm install
pnpm dev
```

> 未安装 pnpm 时先执行 `npm i -g pnpm` 或 `corepack enable`。

浏览器访问 http://localhost:5173 ， 默认账号 `admin / 123456`。

## 主要模块

| 路径 | 功能 |
|------|------|
| `/login` | 登录 |
| `/home` | 首页 |
| `/report` | 报表管理 |
| `/designer/:reportCode` | 大屏设计器 |
| `/preview/:reportCode` | 大屏预览 |
| `/share/:shareCode` | 分享访问 |
| `/dataset` | 数据集 |
| `/datasource` | 数据源 |
| `/dict` | 数据字典 |
| `/access/user` | 用户管理 |
| `/access/role` | 角色管理 |

## 设计器组件

- **文本** / **图片** / **折线图** / **柱状图** / **饼图** / **表格**
- 支持静态数据与数据集驱动
- 拖拽 + 缩放定位
- 右侧属性面板实时调整样式、字段映射等

## 构建

```bash
pnpm build
```

输出到 `dist/`，可由任意静态服务器或 ASP.NET 后端托管。
