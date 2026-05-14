using Microsoft.AspNetCore.Mvc;
using NewLife;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;

namespace Olive.BI.Web.Controllers;

/// <summary>用户角色绑定</summary>
[ApiController]
[Route("access/userRole")]
public class AccessUserRoleController : EntityController
{
    /// <summary>用户角色绑定请求</summary>
    public class BindUserRoleRequest
    {
        /// <summary>登录名</summary>
        public String? LoginName { get; set; }

        /// <summary>角色编码列表</summary>
        public List<String>? RoleCodes { get; set; }
    }

    /// <summary>查询用户已分配角色</summary>
    /// <param name="req">绑定请求（仅使用LoginName）</param>
    /// <returns>角色编码列表</returns>
    [HttpPost("listByLoginName")]
    public Result<List<String>> ListByLoginName([FromBody] BindUserRoleRequest req)
    {
        if (req == null || req.LoginName.IsNullOrEmpty())
            return new Result<List<String>> { State = 1, Msg = "登录名必填" };

        var list = UserRole.FindAll(UserRole._.LoginName == req.LoginName);
        var codes = list.Select(e => e.RoleCode).Where(c => !c.IsNullOrEmpty()).Distinct().ToList()!;

        return ResultHelper.ToResult(codes);
    }

    /// <summary>覆盖式绑定用户角色</summary>
    /// <param name="req">绑定请求</param>
    /// <returns>结果</returns>
    [HttpPost("bind")]
    public Result Bind([FromBody] BindUserRoleRequest req)
    {
        if (req == null || req.LoginName.IsNullOrEmpty())
            return new Result { State = 1, Msg = "登录名必填" };

        var olds = UserRole.FindAll(UserRole._.LoginName == req.LoginName);
        foreach (var item in olds) item.Delete();

        var roles = req.RoleCodes ?? [];
        foreach (var code in roles.Where(c => !c.IsNullOrEmpty()).Distinct())
        {
            new UserRole
            {
                LoginName = req.LoginName!,
                RoleCode = code!,
            }.Insert();
        }

        return new Result().ToResult();
    }
}
