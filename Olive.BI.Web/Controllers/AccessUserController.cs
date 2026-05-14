using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Data;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;
using XCode;
using OliveUser = Olive.BI.Entity.User;

namespace Olive.BI.Web.Controllers;

/// <summary>用户管理</summary>
[ApiController]
[Route("access/user")]
public class AccessUserController : EntityController
{
    /// <summary>分页查询</summary>
    /// <param name="page">分页参数：Index/Size/Keywords</param>
    /// <returns>用户分页列表</returns>
    [HttpPost("listByPage")]
    public Results<UserEntity> ListByPage([FromBody] KeywordsPage page)
    {
        page ??= new KeywordsPage();

        var pp = new PageParameter { PageIndex = page.Index < 1 ? 1 : page.Index, PageSize = page.Size < 1 ? 20 : page.Size };
        var list = OliveUser.SearchPage(page.Keywords, null, pp);
        var models = list.Select(e => e.ToModel()).ToList();

        return ResultHelper.ToResults(models, pp.TotalCount);
    }

    /// <summary>新增用户</summary>
    /// <param name="model">用户模型</param>
    /// <returns>结果</returns>
    [HttpPost("insertUser")]
    public Result InsertUser([FromBody] UserEntity model)
    {
        if (model == null || model.LoginName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
            return new Result { State = 1, Msg = "登录名与密码必填" };

        if (OliveUser.FindByLoginName(model.LoginName!) != null)
            return new Result { State = 1, Msg = "登录名已存在" };

        var entity = new OliveUser();
        entity.Copy(model);
        entity.Password = OliveUser.EncryptPassword(model.Password!);
        if (entity.EnableFlag == 0) entity.EnableFlag = 1;
        entity.DeleteFlag = 0;
        entity.Insert();

        return new Result().ToResult();
    }

    /// <summary>修改用户</summary>
    /// <param name="model">用户模型</param>
    /// <returns>结果</returns>
    [HttpPost("updateUser")]
    public Result UpdateUser([FromBody] UserEntity model)
    {
        if (model == null || model.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = OliveUser.FindById(model.Id);
        if (entity == null) return new Result { State = 1, Msg = "用户不存在" };

        entity.RealName = model.RealName;
        entity.Phone = model.Phone;
        entity.Email = model.Email;
        entity.Remark = model.Remark;
        entity.EnableFlag = model.EnableFlag;
        entity.Update();

        return new Result().ToResult();
    }

    /// <summary>逻辑删除用户</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("deleteUser")]
    public Result DeleteUser([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = OliveUser.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "用户不存在" };

        entity.DeleteFlag = 1;
        entity.Update();

        return new Result().ToResult();
    }

    /// <summary>修改密码请求</summary>
    public class ChangePasswordRequest
    {
        /// <summary>用户Id</summary>
        public Int32 Id { get; set; }

        /// <summary>旧密码</summary>
        public String? OldPassword { get; set; }

        /// <summary>新密码</summary>
        public String? NewPassword { get; set; }
    }

    /// <summary>修改密码</summary>
    /// <param name="req">修改密码请求</param>
    /// <returns>结果</returns>
    [HttpPost("changePassword")]
    public Result ChangePassword([FromBody] ChangePasswordRequest req)
    {
        if (req == null || req.Id <= 0 || req.NewPassword.IsNullOrEmpty())
            return new Result { State = 1, Msg = "参数错误" };

        var entity = OliveUser.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "用户不存在" };

        if (!req.OldPassword.IsNullOrEmpty() && !entity.VerifyPassword(req.OldPassword!))
            return new Result { State = 1, Msg = "原密码错误" };

        entity.Password = OliveUser.EncryptPassword(req.NewPassword!);
        entity.Update();

        return new Result().ToResult();
    }
}
