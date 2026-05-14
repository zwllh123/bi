using Microsoft.AspNetCore.Mvc;
using NewLife;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;

namespace Olive.BI.Web.Controllers;

/// <summary>大屏看板与组件</summary>
[ApiController]
[Route("dashboard")]
public class DashboardController : EntityController
{
    /// <summary>编码请求（兼容前端 JSON: { reportCode: "..." }）</summary>
    public class ReportCodeRequest
    {
        /// <summary>报表编码</summary>
        public String? ReportCode { get; set; }
    }

    /// <summary>大屏全量数据：看板 + 组件</summary>
    public class DashboardDetail
    {
        /// <summary>看板</summary>
        public ReportDashboardEntity? Dashboard { get; set; }

        /// <summary>组件列表</summary>
        public List<ReportDashboardWidgetEntity> Widgets { get; set; } = [];
    }

    /// <summary>按报表编码加载看板全量数据</summary>
    /// <param name="req">报表编码请求</param>
    /// <returns>看板详情</returns>
    [HttpPost("load")]
    public Result<DashboardDetail> Load([FromBody] ReportCodeRequest req)
    {
        if (req == null || req.ReportCode.IsNullOrEmpty())
            return new Result<DashboardDetail> { State = 1, Msg = "报表编码必填" };

        var dash = ReportDashboard.FindByReportCode(req.ReportCode!);
        if (dash == null) return new Result<DashboardDetail> { State = 1, Msg = "看板不存在" };

        var detail = new DashboardDetail
        {
            Dashboard = dash.ToModel(),
            Widgets = dash.GetWidgets().Select(e => e.ToModel()).ToList(),
        };
        return ResultHelper.ToResult(detail);
    }

    /// <summary>保存看板属性</summary>
    /// <param name="model">看板模型</param>
    /// <returns>结果</returns>
    [HttpPost("saveDashboard")]
    public Result SaveDashboard([FromBody] ReportDashboardEntity model)
    {
        if (model == null || model.ReportCode.IsNullOrEmpty())
            return new Result { State = 1, Msg = "报表编码必填" };

        var entity = ReportDashboard.FindByReportCode(model.ReportCode!);
        if (entity == null)
        {
            entity = new ReportDashboard();
            entity.Copy(model);
            if (entity.EnableFlag == 0) entity.EnableFlag = 1;
            entity.Insert();
        }
        else
        {
            entity.Title = model.Title;
            entity.Width = model.Width;
            entity.Height = model.Height;
            entity.BackgroundColor = model.BackgroundColor;
            entity.BackgroundImage = model.BackgroundImage;
            entity.PresetLine = model.PresetLine;
            entity.RefreshSeconds = model.RefreshSeconds;
            entity.EnableFlag = model.EnableFlag;
            entity.Sort = model.Sort;
            entity.Update();
        }
        return new Result().ToResult();
    }

    /// <summary>整页保存组件（全量替换：按报表编码删除旧组件再插入）</summary>
    /// <param name="req">保存请求</param>
    /// <returns>结果</returns>
    [HttpPost("saveWidgets")]
    public Result SaveWidgets([FromBody] SaveWidgetsRequest req)
    {
        if (req == null || req.ReportCode.IsNullOrEmpty())
            return new Result { State = 1, Msg = "报表编码必填" };

        // 物理删除旧组件以避免遗留
        foreach (var w in ReportDashboardWidget.FindAllByReportCode(req.ReportCode!))
        {
            w.Delete();
        }

        if (req.Widgets != null)
        {
            foreach (var m in req.Widgets)
            {
                var entity = new ReportDashboardWidget();
                entity.Copy(m);
                entity.Id = 0;
                entity.ReportCode = req.ReportCode;
                if (entity.EnableFlag == 0) entity.EnableFlag = 1;
                entity.Insert();
            }
        }

        return new Result().ToResult();
    }

    /// <summary>整页保存组件请求</summary>
    public class SaveWidgetsRequest
    {
        /// <summary>报表编码</summary>
        public String? ReportCode { get; set; }

        /// <summary>组件列表</summary>
        public List<ReportDashboardWidgetEntity>? Widgets { get; set; }
    }

    /// <summary>新增/保存单个组件</summary>
    /// <param name="model">组件模型</param>
    /// <returns>新组件Id</returns>
    [HttpPost("widget/save")]
    public Result<Int32> SaveWidget([FromBody] ReportDashboardWidgetEntity model)
    {
        if (model == null || model.ReportCode.IsNullOrEmpty())
            return new Result<Int32> { State = 1, Msg = "报表编码必填" };

        ReportDashboardWidget entity;
        if (model.Id > 0)
        {
            entity = ReportDashboardWidget.FindById(model.Id) ?? new ReportDashboardWidget();
            entity.Type = model.Type;
            entity.Setup = model.Setup;
            entity.Data = model.Data;
            entity.Collapse = model.Collapse;
            entity.Position = model.Position;
            entity.Options = model.Options;
            entity.RefreshSeconds = model.RefreshSeconds;
            entity.EnableFlag = model.EnableFlag;
            entity.Sort = model.Sort;
            if (entity.Id == 0)
            {
                entity.ReportCode = model.ReportCode;
                entity.Insert();
            }
            else entity.Update();
        }
        else
        {
            entity = new ReportDashboardWidget();
            entity.Copy(model);
            entity.Id = 0;
            if (entity.EnableFlag == 0) entity.EnableFlag = 1;
            entity.Insert();
        }
        return ResultHelper.ToResult(entity.Id);
    }

    /// <summary>删除组件（逻辑）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("widget/delete")]
    public Result DeleteWidget([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportDashboardWidget.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "组件不存在" };
        entity.DeleteFlag = 1;
        entity.Update();
        return new Result().ToResult();
    }
}
