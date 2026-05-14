using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Data;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;

namespace Olive.BI.Web.Controllers;

/// <summary>报表管理</summary>
[ApiController]
[Route("report")]
public class ReportController : EntityController
{
    /// <summary>报表分页请求</summary>
    public class ReportPageRequest : KeywordsPage
    {
        /// <summary>报表类型：report_screen/report_excel</summary>
        public String? ReportType { get; set; }

        /// <summary>报表分组</summary>
        public String? ReportGroup { get; set; }

        /// <summary>启用过滤</summary>
        public Int32? EnableFlag { get; set; }
    }

    /// <summary>分页查询报表</summary>
    /// <param name="page">分页参数</param>
    /// <returns>报表分页</returns>
    [HttpPost("listByPage")]
    public Results<ReportEntity> ListByPage([FromBody] ReportPageRequest page)
    {
        page ??= new ReportPageRequest();
        var pp = new PageParameter { PageIndex = page.Index < 1 ? 1 : page.Index, PageSize = page.Size < 1 ? 20 : page.Size };
        var list = Report.SearchPage(page.Keywords, page.ReportType, page.ReportGroup, page.EnableFlag, pp);
        return ResultHelper.ToResults(list.Select(e => e.ToModel()).ToList(), pp.TotalCount);
    }

    /// <summary>新增报表</summary>
    /// <param name="model">报表模型</param>
    /// <returns>结果</returns>
    [HttpPost("insertReport")]
    public Result<ReportEntity> InsertReport([FromBody] ReportEntity model)
    {
        if (model == null || model.ReportCode.IsNullOrEmpty() || model.ReportName.IsNullOrEmpty())
            return new Result<ReportEntity> { State = 1, Msg = "报表编码与名称必填" };
        if (Report.FindByReportCode(model.ReportCode!) != null)
            return new Result<ReportEntity> { State = 1, Msg = "报表编码已存在" };

        var entity = new Report();
        entity.Copy(model);
        if (entity.EnableFlag == 0) entity.EnableFlag = 1;
        entity.Insert();

        // 若是大屏报表，自动创建空看板
        if (entity.ReportType == "report_screen")
        {
            var dash = new ReportDashboard
            {
                ReportCode = entity.ReportCode,
                Title = entity.ReportName,
                Width = 1920,
                Height = 1080,
                EnableFlag = 1,
            };
            dash.Insert();
        }

        return ResultHelper.ToResult(entity.ToModel());
    }

    /// <summary>修改报表</summary>
    /// <param name="model">报表模型</param>
    /// <returns>结果</returns>
    [HttpPost("updateReport")]
    public Result UpdateReport([FromBody] ReportEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = Report.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "报表不存在" };

        entity.ReportName = model.ReportName!;
        entity.ReportGroup = model.ReportGroup;
        entity.ReportType = model.ReportType!;
        entity.ReportImage = model.ReportImage;
        entity.ReportDesc = model.ReportDesc;
        entity.ReportAuthor = model.ReportAuthor;
        entity.EnableFlag = model.EnableFlag;
        entity.Update();
        return new Result().ToResult();
    }

    /// <summary>删除报表（逻辑删除，同时逻辑删除看板与组件）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("deleteReport")]
    public Result DeleteReport([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = Report.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "报表不存在" };

        entity.DeleteFlag = 1;
        entity.Update();

        if (!entity.ReportCode.IsNullOrEmpty())
        {
            var dash = ReportDashboard.FindByReportCode(entity.ReportCode!);
            if (dash != null) { dash.DeleteFlag = 1; dash.Update(); }
            foreach (var w in ReportDashboardWidget.FindAllByReportCode(entity.ReportCode!))
            {
                if (w.DeleteFlag == 0) { w.DeleteFlag = 1; w.Update(); }
            }
        }

        return new Result().ToResult();
    }

    /// <summary>按编码查询报表</summary>
    /// <param name="req">编码请求</param>
    /// <returns>报表</returns>
    [HttpPost("getByCode")]
    public Result<ReportEntity> GetByCode([FromBody] DataSetController.CodeRequest req)
    {
        if (req == null || req.Code.IsNullOrEmpty())
            return new Result<ReportEntity> { State = 1, Msg = "编码必填" };
        var entity = Report.FindByReportCode(req.Code!);
        if (entity == null) return new Result<ReportEntity> { State = 1, Msg = "报表不存在" };
        return ResultHelper.ToResult(entity.ToModel());
    }
}
