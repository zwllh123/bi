using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Data;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;

namespace Olive.BI.Web.Controllers;

/// <summary>数据集管理</summary>
[ApiController]
[Route("dataSet")]
public class DataSetController : EntityController
{
    /// <summary>数据集分页请求</summary>
    public class DataSetPageRequest : KeywordsPage
    {
        /// <summary>数据源编码过滤</summary>
        public String? SourceCode { get; set; }

        /// <summary>启用过滤</summary>
        public Int32? EnableFlag { get; set; }
    }

    /// <summary>分页查询数据集</summary>
    /// <param name="page">分页参数</param>
    /// <returns>数据集分页</returns>
    [HttpPost("listByPage")]
    public Results<ReportDataSetEntity> ListByPage([FromBody] DataSetPageRequest page)
    {
        page ??= new DataSetPageRequest();
        var pp = new PageParameter { PageIndex = page.Index < 1 ? 1 : page.Index, PageSize = page.Size < 1 ? 20 : page.Size };
        var list = ReportDataSet.SearchPage(page.Keywords, page.SourceCode, page.EnableFlag, pp);
        return ResultHelper.ToResults(list.Select(e => e.ToModel()).ToList(), pp.TotalCount);
    }

    /// <summary>新增数据集</summary>
    /// <param name="model">数据集模型</param>
    /// <returns>结果</returns>
    [HttpPost("insertSet")]
    public Result InsertSet([FromBody] ReportDataSetEntity model)
    {
        if (model == null || model.SetCode.IsNullOrEmpty() || model.SetName.IsNullOrEmpty())
            return new Result { State = 1, Msg = "数据集编码与名称必填" };
        if (ReportDataSet.FindBySetCode(model.SetCode!) != null)
            return new Result { State = 1, Msg = "数据集编码已存在" };

        var entity = new ReportDataSet();
        entity.Copy(model);
        if (entity.EnableFlag == 0) entity.EnableFlag = 1;
        entity.Insert();
        return new Result().ToResult();
    }

    /// <summary>修改数据集</summary>
    /// <param name="model">数据集模型</param>
    /// <returns>结果</returns>
    [HttpPost("updateSet")]
    public Result UpdateSet([FromBody] ReportDataSetEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportDataSet.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "数据集不存在" };

        entity.SetName = model.SetName!;
        entity.SetDesc = model.SetDesc;
        entity.SourceCode = model.SourceCode!;
        entity.DynSentence = model.DynSentence;
        entity.CaseResult = model.CaseResult;
        entity.SetType = model.SetType!;
        entity.EnableFlag = model.EnableFlag;
        entity.Update();
        return new Result().ToResult();
    }

    /// <summary>删除数据集（逻辑删除）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("deleteSet")]
    public Result DeleteSet([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportDataSet.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "数据集不存在" };
        entity.DeleteFlag = 1;
        entity.Update();
        return new Result().ToResult();
    }

    /// <summary>查询数据集详情（含参数与转换）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>详情</returns>
    [HttpPost("detail")]
    public Result<DataSetDetail> Detail([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result<DataSetDetail> { State = 1, Msg = "Id必填" };
        var entity = ReportDataSet.FindById(req.Id);
        if (entity == null) return new Result<DataSetDetail> { State = 1, Msg = "数据集不存在" };

        var detail = new DataSetDetail
        {
            DataSet = entity.ToModel(),
            Params = entity.GetParams().Select(e => e.ToModel()).ToList(),
            Transforms = entity.GetTransforms().Select(e => e.ToModel()).ToList(),
        };
        return ResultHelper.ToResult(detail);
    }

    /// <summary>数据集详情</summary>
    public class DataSetDetail
    {
        /// <summary>数据集主体</summary>
        public ReportDataSetEntity? DataSet { get; set; }

        /// <summary>参数列表</summary>
        public List<ReportDataSetParamEntity> Params { get; set; } = [];

        /// <summary>转换列表</summary>
        public List<ReportDataSetTransformEntity> Transforms { get; set; } = [];
    }

    /// <summary>执行数据集（前端预览/调试用）</summary>
    /// <param name="req">执行请求</param>
    /// <returns>执行结果</returns>
    [HttpPost("execute")]
    public Result<SqlExecuteResult> Execute([FromBody] ExecuteSetRequest req)
    {
        if (req == null || req.SetCode.IsNullOrEmpty())
            return new Result<SqlExecuteResult> { State = 1, Msg = "数据集编码必填" };

        var entity = ReportDataSet.FindBySetCode(req.SetCode!);
        if (entity == null) return new Result<SqlExecuteResult> { State = 1, Msg = "数据集不存在" };
        if (entity.EnableFlag != 1) return new Result<SqlExecuteResult> { State = 1, Msg = "数据集已禁用" };

        try
        {
            var result = entity.ExecuteSql(req.Parameters, req.MaxRows <= 0 ? 1000 : req.MaxRows);
            return ResultHelper.ToResult(result);
        }
        catch (Exception ex)
        {
            return new Result<SqlExecuteResult> { State = 1, Msg = "数据集执行失败：" + ex.Message };
        }
    }

    /// <summary>执行数据集请求</summary>
    public class ExecuteSetRequest
    {
        /// <summary>数据集编码</summary>
        public String? SetCode { get; set; }

        /// <summary>参数</summary>
        public Dictionary<String, Object?>? Parameters { get; set; }

        /// <summary>最大行数</summary>
        public Int32 MaxRows { get; set; } = 1000;
    }

    #region 数据集参数

    /// <summary>查询数据集参数列表</summary>
    /// <param name="req">数据集编码请求（Keywords=数据集编码）</param>
    /// <returns>参数列表</returns>
    [HttpPost("param/listBySet")]
    public Result<List<ReportDataSetParamEntity>> ListParamBySet([FromBody] CodeRequest req)
    {
        if (req == null || req.Code.IsNullOrEmpty())
            return new Result<List<ReportDataSetParamEntity>> { State = 1, Msg = "数据集编码必填" };
        var list = ReportDataSetParam.ListBySet(req.Code!);
        return ResultHelper.ToResult(list.Select(e => e.ToModel()).ToList());
    }

    /// <summary>新增参数</summary>
    /// <param name="model">参数模型</param>
    /// <returns>结果</returns>
    [HttpPost("param/insert")]
    public Result InsertParam([FromBody] ReportDataSetParamEntity model)
    {
        if (model == null || model.SetCode.IsNullOrEmpty() || model.ParamName.IsNullOrEmpty())
            return new Result { State = 1, Msg = "数据集编码与参数名必填" };
        var entity = new ReportDataSetParam();
        entity.Copy(model);
        entity.Insert();
        return new Result().ToResult();
    }

    /// <summary>修改参数</summary>
    /// <param name="model">参数模型</param>
    /// <returns>结果</returns>
    [HttpPost("param/update")]
    public Result UpdateParam([FromBody] ReportDataSetParamEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportDataSetParam.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "参数不存在" };

        entity.ParamName = model.ParamName!;
        entity.ParamDesc = model.ParamDesc;
        entity.ParamType = model.ParamType;
        entity.SampleItem = model.SampleItem;
        entity.RequiredFlag = model.RequiredFlag;
        entity.ValidationRules = model.ValidationRules;
        entity.OrderNum = model.OrderNum;
        entity.Update();
        return new Result().ToResult();
    }

    /// <summary>删除参数（物理）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("param/delete")]
    public Result DeleteParam([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportDataSetParam.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "参数不存在" };
        entity.Delete();
        return new Result().ToResult();
    }

    #endregion

    #region 数据集转换

    /// <summary>查询数据集转换列表</summary>
    /// <param name="req">数据集编码请求</param>
    /// <returns>转换列表</returns>
    [HttpPost("transform/listBySet")]
    public Result<List<ReportDataSetTransformEntity>> ListTransformBySet([FromBody] CodeRequest req)
    {
        if (req == null || req.Code.IsNullOrEmpty())
            return new Result<List<ReportDataSetTransformEntity>> { State = 1, Msg = "数据集编码必填" };
        var list = ReportDataSetTransform.ListBySet(req.Code!);
        return ResultHelper.ToResult(list.Select(e => e.ToModel()).ToList());
    }

    /// <summary>新增转换</summary>
    /// <param name="model">转换模型</param>
    /// <returns>结果</returns>
    [HttpPost("transform/insert")]
    public Result InsertTransform([FromBody] ReportDataSetTransformEntity model)
    {
        if (model == null || model.SetCode.IsNullOrEmpty() || model.TransformType.IsNullOrEmpty())
            return new Result { State = 1, Msg = "数据集编码与转换类型必填" };
        var entity = new ReportDataSetTransform();
        entity.Copy(model);
        if (entity.EnableFlag == 0) entity.EnableFlag = 1;
        entity.Insert();
        return new Result().ToResult();
    }

    /// <summary>修改转换</summary>
    /// <param name="model">转换模型</param>
    /// <returns>结果</returns>
    [HttpPost("transform/update")]
    public Result UpdateTransform([FromBody] ReportDataSetTransformEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportDataSetTransform.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "转换不存在" };

        entity.TransformType = model.TransformType!;
        entity.TransformScript = model.TransformScript;
        entity.OrderNum = model.OrderNum;
        entity.EnableFlag = model.EnableFlag;
        entity.Update();
        return new Result().ToResult();
    }

    /// <summary>删除转换（逻辑）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("transform/delete")]
    public Result DeleteTransform([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportDataSetTransform.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "转换不存在" };
        entity.DeleteFlag = 1;
        entity.Update();
        return new Result().ToResult();
    }

    #endregion

    /// <summary>按编码请求</summary>
    public class CodeRequest
    {
        /// <summary>编码</summary>
        public String? Code { get; set; }
    }
}
