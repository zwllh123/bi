using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Data;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;

namespace Olive.BI.Web.Controllers;

/// <summary>数据字典管理</summary>
[ApiController]
[Route("dict")]
public class DictController : EntityController
{
    /// <summary>字典分页</summary>
    /// <param name="page">分页参数</param>
    /// <returns>字典分页</returns>
    [HttpPost("listByPage")]
    public Results<DictEntity> ListByPage([FromBody] KeywordsPage page)
    {
        page ??= new KeywordsPage();
        var pp = new PageParameter { PageIndex = page.Index < 1 ? 1 : page.Index, PageSize = page.Size < 1 ? 20 : page.Size };
        var list = Dict.SearchPage(page.Keywords, pp);
        var models = list.Select(e => e.ToModel()).ToList();

        return ResultHelper.ToResults(models, pp.TotalCount);
    }

    /// <summary>新增字典</summary>
    /// <param name="model">字典模型</param>
    /// <returns>结果</returns>
    [HttpPost("insertDict")]
    public Result InsertDict([FromBody] DictEntity model)
    {
        if (model == null || model.DictCode.IsNullOrEmpty() || model.DictName.IsNullOrEmpty())
            return new Result { State = 1, Msg = "字典编码与名称必填" };

        if (Dict.FindByDictCode(model.DictCode!) != null)
            return new Result { State = 1, Msg = "字典编码已存在" };

        var entity = new Dict();
        entity.Copy(model);
        entity.Insert();

        return new Result().ToResult();
    }

    /// <summary>修改字典</summary>
    /// <param name="model">字典模型</param>
    /// <returns>结果</returns>
    [HttpPost("updateDict")]
    public Result UpdateDict([FromBody] DictEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = Dict.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "字典不存在" };

        entity.DictName = model.DictName!;
        entity.Remark = model.Remark;
        entity.Update();

        return new Result().ToResult();
    }

    /// <summary>删除字典（物理删除，同时删除字典项）</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("deleteDict")]
    public Result DeleteDict([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = Dict.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "字典不存在" };

        var items = DictItem.FindAllByDictCode(entity.DictCode);
        foreach (var item in items) item.Delete();
        entity.Delete();

        return new Result().ToResult();
    }

    /// <summary>字典项分页（含字典编码过滤）</summary>
    /// <param name="page">分页参数：Keywords=字典编码可空</param>
    /// <returns>字典项分页</returns>
    [HttpPost("item/listByPage")]
    public Results<DictItemEntity> ListItemByPage([FromBody] DictItemPageRequest page)
    {
        page ??= new DictItemPageRequest();
        var pp = new PageParameter { PageIndex = page.Index < 1 ? 1 : page.Index, PageSize = page.Size < 1 ? 50 : page.Size };
        var list = DictItem.SearchPage(page.DictCode, page.Keywords, page.Enabled, pp);
        var models = list.Select(e => e.ToModel()).ToList();

        return ResultHelper.ToResults(models, pp.TotalCount);
    }

    /// <summary>字典项分页请求</summary>
    public class DictItemPageRequest : KeywordsPage
    {
        /// <summary>字典编码</summary>
        public String? DictCode { get; set; }

        /// <summary>启用过滤</summary>
        public Int32? Enabled { get; set; }
    }

    /// <summary>按字典编码查询全部启用项（前端下拉用）</summary>
    /// <param name="req">字典编码请求</param>
    /// <returns>启用字典项列表</returns>
    [HttpPost("item/listByDictCode")]
    public Result<List<DictItemEntity>> ListByDictCode([FromBody] DictItemPageRequest req)
    {
        if (req == null || req.DictCode.IsNullOrEmpty())
            return new Result<List<DictItemEntity>> { State = 1, Msg = "字典编码必填" };

        var list = DictItem.FindAllByDictCode(req.DictCode);
        var models = list.Where(e => e.Enabled == 1).Select(e => e.ToModel()).ToList();

        return ResultHelper.ToResult(models);
    }

    /// <summary>新增字典项</summary>
    /// <param name="model">字典项模型</param>
    /// <returns>结果</returns>
    [HttpPost("item/insertItem")]
    public Result InsertItem([FromBody] DictItemEntity model)
    {
        if (model == null || model.DictCode.IsNullOrEmpty() || model.ItemName.IsNullOrEmpty() || model.ItemValue.IsNullOrEmpty())
            return new Result { State = 1, Msg = "字典编码/项名/项值必填" };

        var entity = new DictItem();
        entity.Copy(model);
        if (entity.Enabled == 0) entity.Enabled = 1;
        entity.Insert();

        return new Result().ToResult();
    }

    /// <summary>修改字典项</summary>
    /// <param name="model">字典项模型</param>
    /// <returns>结果</returns>
    [HttpPost("item/updateItem")]
    public Result UpdateItem([FromBody] DictItemEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = DictItem.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "字典项不存在" };

        entity.ItemName = model.ItemName!;
        entity.ItemValue = model.ItemValue!;
        entity.ItemExtend = model.ItemExtend;
        entity.Enabled = model.Enabled;
        entity.Locale = model.Locale;
        entity.Remark = model.Remark;
        entity.Sort = model.Sort;
        entity.Update();

        return new Result().ToResult();
    }

    /// <summary>删除字典项</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("item/deleteItem")]
    public Result DeleteItem([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = DictItem.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "字典项不存在" };

        entity.Delete();
        return new Result().ToResult();
    }
}
