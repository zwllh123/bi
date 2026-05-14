using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Data;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;
using XCode;

namespace Olive.BI.Web.Controllers;

/// <summary>权限菜单管理</summary>
[ApiController]
[Route("access/authority")]
public class AccessAuthorityController : EntityController
{
    /// <summary>权限树</summary>
    /// <returns>权限树</returns>
    [HttpPost("tree")]
    public Result<List<AuthorityNode>> Tree() => ResultHelper.ToResult(Authority.GetTree());

    /// <summary>分页查询</summary>
    /// <param name="page">分页参数</param>
    /// <returns>权限分页</returns>
    [HttpPost("listByPage")]
    public Results<AuthorityEntity> ListByPage([FromBody] KeywordsPage page)
    {
        page ??= new KeywordsPage();
        var pp = new PageParameter { PageIndex = page.Index < 1 ? 1 : page.Index, PageSize = page.Size < 1 ? 20 : page.Size };
        var list = Authority.SearchPage(page.Keywords, null, pp);
        var models = list.Select(e => e.ToModel()).ToList();

        return ResultHelper.ToResults(models, pp.TotalCount);
    }

    /// <summary>新增</summary>
    /// <param name="model">权限模型</param>
    /// <returns>结果</returns>
    [HttpPost("insertAuthority")]
    public Result InsertAuthority([FromBody] AuthorityEntity model)
    {
        if (model == null || model.Target.IsNullOrEmpty())
            return new Result { State = 1, Msg = "菜单代码必填" };

        if (Authority.FindByTargetAndAction(model.Target!, model.Action ?? "") != null)
            return new Result { State = 1, Msg = "菜单+按钮已存在" };

        var entity = new Authority();
        entity.Copy(model);
        if (entity.EnableFlag == 0) entity.EnableFlag = 1;
        entity.DeleteFlag = 0;
        entity.Insert();

        return new Result().ToResult();
    }

    /// <summary>修改</summary>
    /// <param name="model">权限模型</param>
    /// <returns>结果</returns>
    [HttpPost("updateAuthority")]
    public Result UpdateAuthority([FromBody] AuthorityEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = Authority.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "权限不存在" };

        entity.ParentTarget = model.ParentTarget;
        entity.TargetName = model.TargetName!;
        entity.ActionName = model.ActionName!;
        entity.Sort = model.Sort;
        entity.EnableFlag = model.EnableFlag;
        entity.Update();

        return new Result().ToResult();
    }

    /// <summary>逻辑删除</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("deleteAuthority")]
    public Result DeleteAuthority([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = Authority.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "权限不存在" };

        entity.DeleteFlag = 1;
        entity.Update();

        return new Result().ToResult();
    }
}
