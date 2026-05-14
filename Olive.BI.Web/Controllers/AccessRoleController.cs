using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Data;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;
using XCode;

namespace Olive.BI.Web.Controllers;

/// <summary>角色管理</summary>
[ApiController]
[Route("access/role")]
public class AccessRoleController : EntityController
{
    /// <summary>分页查询角色</summary>
    /// <param name="page">分页参数</param>
    /// <returns>角色分页</returns>
    [HttpPost("listByPage")]
    public Results<RoleEntity> ListByPage([FromBody] KeywordsPage page)
    {
        page ??= new KeywordsPage();
        var pp = new PageParameter { PageIndex = page.Index < 1 ? 1 : page.Index, PageSize = page.Size < 1 ? 20 : page.Size };
        var list = Role.SearchPage(page.Keywords, null, pp);
        var models = list.Select(e => e.ToModel()).ToList();

        return ResultHelper.ToResults(models, pp.TotalCount);
    }

    /// <summary>所有启用角色</summary>
    /// <returns>角色列表</returns>
    [HttpPost("list")]
    public Result<List<RoleEntity>> List()
    {
        var list = Role.FindAll(Role._.DeleteFlag == 0 & Role._.EnableFlag == 1);
        var models = list.Select(e => e.ToModel()).ToList();

        return ResultHelper.ToResult(models);
    }

    /// <summary>新增角色</summary>
    /// <param name="model">角色模型</param>
    /// <returns>结果</returns>
    [HttpPost("insertRole")]
    public Result InsertRole([FromBody] RoleEntity model)
    {
        if (model == null || model.RoleCode.IsNullOrEmpty() || model.RoleName.IsNullOrEmpty())
            return new Result { State = 1, Msg = "角色编码与名称必填" };

        if (Role.FindByRoleCode(model.RoleCode!) != null)
            return new Result { State = 1, Msg = "角色编码已存在" };

        var entity = new Role();
        entity.Copy(model);
        if (entity.EnableFlag == 0) entity.EnableFlag = 1;
        entity.DeleteFlag = 0;
        entity.Insert();

        return new Result().ToResult();
    }

    /// <summary>修改角色</summary>
    /// <param name="model">角色模型</param>
    /// <returns>结果</returns>
    [HttpPost("updateRole")]
    public Result UpdateRole([FromBody] RoleEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = Role.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "角色不存在" };

        entity.RoleName = model.RoleName!;
        entity.EnableFlag = model.EnableFlag;
        entity.Update();

        return new Result().ToResult();
    }

    /// <summary>逻辑删除角色</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("deleteRole")]
    public Result DeleteRole([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = Role.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "角色不存在" };

        entity.DeleteFlag = 1;
        entity.Update();

        return new Result().ToResult();
    }

    /// <summary>授权请求：覆盖角色的权限列表</summary>
    public class GrantAuthorityRequest
    {
        /// <summary>角色编码</summary>
        public String? RoleCode { get; set; }

        /// <summary>权限项列表：每项形如 "Target:Action"</summary>
        public List<String>? Authorities { get; set; }
    }

    /// <summary>授权：以传入权限列表覆盖角色当前权限</summary>
    /// <param name="req">授权请求</param>
    /// <returns>结果</returns>
    [HttpPost("grantAuthority")]
    public Result GrantAuthority([FromBody] GrantAuthorityRequest req)
    {
        if (req == null || req.RoleCode.IsNullOrEmpty()) return new Result { State = 1, Msg = "角色编码必填" };

        var role = Role.FindByRoleCode(req.RoleCode!);
        if (role == null) return new Result { State = 1, Msg = "角色不存在" };

        // 先清除该角色现有权限
        var olds = RoleAuthority.FindAll(RoleAuthority._.RoleCode == req.RoleCode);
        foreach (var item in olds) item.Delete();

        // 写入新的权限
        var auths = req.Authorities ?? [];
        foreach (var pair in auths.Where(e => !e.IsNullOrEmpty()).Distinct())
        {
            var parts = pair!.Split(':');
            var target = parts.Length > 0 ? parts[0] : "";
            var action = parts.Length > 1 ? parts[1] : "";
            if (target.IsNullOrEmpty()) continue;

            new RoleAuthority
            {
                RoleCode = req.RoleCode!,
                Target = target,
                Action = action,
            }.Insert();
        }

        return new Result().ToResult();
    }

    /// <summary>查询角色拥有的权限</summary>
    /// <param name="req">Id请求（此处传 RoleCode 不便，使用 IdsRequest 的字符串模式可选；此处用专用请求）</param>
    /// <returns>权限列表</returns>
    [HttpPost("listAuthority")]
    public Result<List<RoleAuthorityEntity>> ListAuthority([FromBody] GrantAuthorityRequest req)
    {
        if (req == null || req.RoleCode.IsNullOrEmpty())
            return new Result<List<RoleAuthorityEntity>> { State = 1, Msg = "角色编码必填" };

        var list = RoleAuthority.FindAll(RoleAuthority._.RoleCode == req.RoleCode);
        var models = list.Select(e => e.ToModel()).ToList();

        return ResultHelper.ToResult(models);
    }
}
