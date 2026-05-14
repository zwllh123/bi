using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Data;
using NewLife.Serialization;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;

namespace Olive.BI.Web.Controllers;

/// <summary>数据源管理</summary>
[ApiController]
[Route("dataSource")]
public class DataSourceController : EntityController
{
    /// <summary>数据源分页请求</summary>
    public class DataSourcePageRequest : KeywordsPage
    {
        /// <summary>启用标记</summary>
        public Int32? EnableFlag { get; set; }
    }

    /// <summary>分页查询数据源</summary>
    /// <param name="page">分页参数</param>
    /// <returns>数据源分页</returns>
    [HttpPost("listByPage")]
    public Results<ReportDataSourceEntity> ListByPage([FromBody] DataSourcePageRequest page)
    {
        page ??= new DataSourcePageRequest();
        var pp = new PageParameter { PageIndex = page.Index < 1 ? 1 : page.Index, PageSize = page.Size < 1 ? 20 : page.Size };
        var list = ReportDataSource.SearchPage(page.Keywords, page.EnableFlag, pp);
        var models = list.Select(e => e.ToModel()).ToList();
        return ResultHelper.ToResults(models, pp.TotalCount);
    }

    /// <summary>查询所有启用的数据源（下拉列表用）</summary>
    /// <returns>数据源列表</returns>
    [HttpPost("listAll")]
    public Result<List<ReportDataSourceEntity>> ListAll()
    {
        var pp = new PageParameter { PageIndex = 1, PageSize = 1000 };
        var list = ReportDataSource.SearchPage(null, 1, pp);
        return ResultHelper.ToResult(list.Select(e => e.ToModel()).ToList());
    }

    /// <summary>新增数据源</summary>
    /// <param name="model">数据源模型</param>
    /// <returns>结果</returns>
    [HttpPost("insertSource")]
    public Result InsertSource([FromBody] ReportDataSourceEntity model)
    {
        if (model == null || model.SourceCode.IsNullOrEmpty() || model.SourceName.IsNullOrEmpty())
            return new Result { State = 1, Msg = "数据源编码与名称必填" };

        if (ReportDataSource.FindBySourceCode(model.SourceCode!) != null)
            return new Result { State = 1, Msg = "数据源编码已存在" };

        var entity = new ReportDataSource();
        entity.Copy(model);
        if (entity.EnableFlag == 0) entity.EnableFlag = 1;
        entity.Insert();

        return new Result().ToResult();
    }

    /// <summary>修改数据源</summary>
    /// <param name="model">数据源模型</param>
    /// <returns>结果</returns>
    [HttpPost("updateSource")]
    public Result UpdateSource([FromBody] ReportDataSourceEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = ReportDataSource.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "数据源不存在" };

        entity.SourceName = model.SourceName!;
        entity.SourceDesc = model.SourceDesc;
        entity.SourceType = model.SourceType!;
        entity.SourceConfig = model.SourceConfig;
        entity.EnableFlag = model.EnableFlag;
        entity.Update();

        return new Result().ToResult();
    }

    /// <summary>删除数据源（逻辑删除）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("deleteSource")]
    public Result DeleteSource([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = ReportDataSource.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "数据源不存在" };

        entity.DeleteFlag = 1;
        entity.Update();
        return new Result().ToResult();
    }

    /// <summary>测试连接</summary>
    /// <param name="req">数据源ID或编码</param>
    /// <returns>测试结果</returns>
    [HttpPost("testConnection")]
    public Result TestConnection([FromBody] TestConnectionRequest req)
    {
        if (req == null) return new Result { State = 1, Msg = "请求为空" };

        ReportDataSource? entity = null;
        // 1. 优先按完整实体（含临时未保存配置）测试
        if (!req.SourceConfig.IsNullOrEmpty())
        {
            entity = new ReportDataSource
            {
                SourceCode = req.SourceCode.IsNullOrEmpty() ? "test_" + Guid.NewGuid().ToString("N")[..8] : req.SourceCode!,
                SourceType = req.SourceType ?? "mysql",
                SourceConfig = req.SourceConfig,
            };
        }
        else if (req.Id > 0)
        {
            entity = ReportDataSource.FindById(req.Id);
        }
        else if (!req.SourceCode.IsNullOrEmpty())
        {
            entity = ReportDataSource.FindBySourceCode(req.SourceCode!);
        }

        if (entity == null) return new Result { State = 1, Msg = "数据源不存在" };

        var err = entity.TestConnection();
        return err == null
            ? new Result { State = 0, Msg = "连接成功" }
            : new Result { State = 1, Msg = "连接失败：" + err };
    }

    /// <summary>测试连接请求</summary>
    public class TestConnectionRequest
    {
        /// <summary>数据源Id</summary>
        public Int32 Id { get; set; }

        /// <summary>数据源编码</summary>
        public String? SourceCode { get; set; }

        /// <summary>数据源类型</summary>
        public String? SourceType { get; set; }

        /// <summary>连接配置 JSON</summary>
        public String? SourceConfig { get; set; }
    }

    /// <summary>执行 SQL 请求</summary>
    public class ExecuteSqlRequest
    {
        /// <summary>数据源编码</summary>
        public String? SourceCode { get; set; }

        /// <summary>SQL</summary>
        public String? Sql { get; set; }

        /// <summary>参数字典</summary>
        public Dictionary<String, Object?>? Parameters { get; set; }

        /// <summary>最大返回行数</summary>
        public Int32 MaxRows { get; set; } = 1000;
    }

    /// <summary>执行 SQL</summary>
    /// <param name="req">请求</param>
    /// <returns>结果</returns>
    [HttpPost("executeSql")]
    public Result<SqlExecuteResult> ExecuteSql([FromBody] ExecuteSqlRequest req)
    {
        if (req == null || req.SourceCode.IsNullOrEmpty() || req.Sql.IsNullOrEmpty())
            return new Result<SqlExecuteResult> { State = 1, Msg = "数据源编码与SQL必填" };

        var entity = ReportDataSource.FindBySourceCode(req.SourceCode!);
        if (entity == null) return new Result<SqlExecuteResult> { State = 1, Msg = "数据源不存在" };
        if (entity.EnableFlag != 1) return new Result<SqlExecuteResult> { State = 1, Msg = "数据源已禁用" };

        try
        {
            var data = entity.Execute(req.Sql!, req.Parameters, req.MaxRows);
            return ResultHelper.ToResult(data);
        }
        catch (Exception ex)
        {
            return new Result<SqlExecuteResult> { State = 1, Msg = "SQL 执行失败：" + ex.Message };
        }
    }
}
