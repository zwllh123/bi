using Microsoft.AspNetCore.Mvc;
using NewLife;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;

namespace Olive.BI.Web.Controllers;

/// <summary>Excel 报表</summary>
[ApiController]
[Route("excel")]
public class ExcelReportController : EntityController
{
    /// <summary>按报表编码获取 Excel 报表配置</summary>
    /// <param name="req">报表编码请求</param>
    /// <returns>Excel 报表</returns>
    [HttpPost("getByReportCode")]
    public Result<ReportExcelEntity> GetByReportCode([FromBody] DashboardController.ReportCodeRequest req)
    {
        if (req == null || req.ReportCode.IsNullOrEmpty())
            return new Result<ReportExcelEntity> { State = 1, Msg = "报表编码必填" };

        var entity = ReportExcel.FindByReportCode(req.ReportCode!);
        if (entity == null) return new Result<ReportExcelEntity> { State = 1, Msg = "Excel报表不存在" };
        return ResultHelper.ToResult(entity.ToModel());
    }

    /// <summary>保存（新增或更新）Excel 报表配置</summary>
    /// <param name="model">Excel 报表模型</param>
    /// <returns>结果</returns>
    [HttpPost("save")]
    public Result Save([FromBody] ReportExcelEntity model)
    {
        if (model == null || model.ReportCode.IsNullOrEmpty())
            return new Result { State = 1, Msg = "报表编码必填" };

        var entity = ReportExcel.FindByReportCode(model.ReportCode!);
        if (entity == null)
        {
            entity = new ReportExcel();
            entity.Copy(model);
            entity.Id = 0;
            if (entity.EnableFlag == 0) entity.EnableFlag = 1;
            entity.Insert();
        }
        else
        {
            entity.SetCodes = model.SetCodes;
            entity.SetParam = model.SetParam;
            entity.JsonStr = model.JsonStr;
            entity.EnableFlag = model.EnableFlag;
            entity.Update();
        }

        return new Result().ToResult();
    }

    /// <summary>删除（逻辑）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("delete")]
    public Result Delete([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportExcel.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "Excel报表不存在" };
        entity.DeleteFlag = 1;
        entity.Update();
        return new Result().ToResult();
    }

    /// <summary>Excel 执行请求：使用多数据集执行并返回</summary>
    public class ExecuteExcelRequest
    {
        /// <summary>报表编码</summary>
        public String? ReportCode { get; set; }

        /// <summary>参数</summary>
        public Dictionary<String, Object?>? Parameters { get; set; }
    }

    /// <summary>执行 Excel 报表所有数据集</summary>
    /// <param name="req">执行请求</param>
    /// <returns>各数据集结果字典：SetCode → SqlExecuteResult</returns>
    [HttpPost("execute")]
    public Result<Dictionary<String, SqlExecuteResult>> Execute([FromBody] ExecuteExcelRequest req)
    {
        if (req == null || req.ReportCode.IsNullOrEmpty())
            return new Result<Dictionary<String, SqlExecuteResult>> { State = 1, Msg = "报表编码必填" };

        var entity = ReportExcel.FindByReportCode(req.ReportCode!);
        if (entity == null) return new Result<Dictionary<String, SqlExecuteResult>> { State = 1, Msg = "Excel报表不存在" };

        var dict = new Dictionary<String, SqlExecuteResult>();
        if (entity.SetCodes.IsNullOrEmpty()) return ResultHelper.ToResult(dict);

        var codes = entity.SetCodes!.Split('|', StringSplitOptions.RemoveEmptyEntries);
        foreach (var code in codes)
        {
            var setEntity = ReportDataSet.FindBySetCode(code);
            if (setEntity == null || setEntity.EnableFlag != 1) continue;
            try
            {
                dict[code] = setEntity.ExecuteSql(req.Parameters, 5000);
            }
            catch (Exception ex)
            {
                dict[code] = new SqlExecuteResult { Columns = ["__error__"], Rows = [new Dictionary<String, Object?> { ["__error__"] = ex.Message }] };
            }
        }

        return ResultHelper.ToResult(dict);
    }
}
