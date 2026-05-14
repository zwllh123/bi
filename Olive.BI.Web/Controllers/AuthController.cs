using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Web;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;
using OliveUser = Olive.BI.Entity.User;

namespace Olive.BI.Web.Controllers;

/// <summary>认证接口。登录、登出、当前用户信息</summary>
[ApiController]
[Route("auth")]
public class AuthController : EntityController
{
    private readonly IConfiguration _config;

    /// <summary>构造</summary>
    /// <param name="config">配置</param>
    public AuthController(IConfiguration config) => _config = config;

    /// <summary>登录请求</summary>
    public class LoginRequest
    {
        /// <summary>登录名</summary>
        public String? LoginName { get; set; }

        /// <summary>密码（明文，前端建议传输前已做一次MD5）</summary>
        public String? Password { get; set; }
    }

    /// <summary>登录响应</summary>
    public class LoginResponse
    {
        /// <summary>Token</summary>
        public String Token { get; set; } = null!;

        /// <summary>登录名</summary>
        public String LoginName { get; set; } = null!;

        /// <summary>真实姓名</summary>
        public String? RealName { get; set; }

        /// <summary>角色编码列表</summary>
        public IList<String> Roles { get; set; } = [];
    }

    /// <summary>登录</summary>
    /// <param name="req">登录请求</param>
    /// <returns>Token 与用户信息</returns>
    [HttpPost("login")]
    public Result<LoginResponse> Login([FromBody] LoginRequest req)
    {
        if (req == null || req.LoginName.IsNullOrEmpty() || req.Password.IsNullOrEmpty())
            return new Result<LoginResponse> { State = 1, Msg = "登录名或密码不能为空" };

        var user = OliveUser.FindByLoginName(req.LoginName!);
        if (user == null || user.DeleteFlag != 0)
            return new Result<LoginResponse> { State = 1, Msg = "用户不存在" };

        if (user.EnableFlag != 1)
            return new Result<LoginResponse> { State = 1, Msg = "用户已禁用" };

        if (!user.VerifyPassword(req.Password!))
            return new Result<LoginResponse> { State = 1, Msg = "密码错误" };

        // 更新最后登录信息
        user.LastLoginTime = DateTime.Now;
        user.LastLoginIP = GetIp();
        user.Update();

        var token = BuildToken(user.LoginName!);
        var resp = new LoginResponse
        {
            Token = token,
            LoginName = user.LoginName!,
            RealName = user.RealName,
            Roles = user.GetRoleCodes(),
        };

        return ResultHelper.ToResult(resp);
    }

    /// <summary>登出</summary>
    /// <returns>结果</returns>
    [HttpPost("logout")]
    public Result Logout() => new Result().ToResult();

    /// <summary>当前登录用户信息</summary>
    /// <returns>用户信息</returns>
    [HttpGet("me")]
    public Result<LoginResponse> Me()
    {
        var loginName = HttpContext.User?.Identity?.Name;
        if (loginName.IsNullOrEmpty())
            return new Result<LoginResponse> { State = 401, Msg = "未登录" };

        var user = OliveUser.FindByLoginName(loginName!);
        if (user == null)
            return new Result<LoginResponse> { State = 401, Msg = "用户不存在" };

        return ResultHelper.ToResult(new LoginResponse
        {
            Token = "",
            LoginName = user.LoginName!,
            RealName = user.RealName,
            Roles = user.GetRoleCodes(),
        });
    }

    private String BuildToken(String loginName)
    {
        var section = _config.GetSection("Jwt");
        var secret = section["Secret"] ?? "OliveBI-Default-Secret";
        var issuer = section["Issuer"] ?? "OliveBI";
        var expire = section.GetValue<Int32?>("Expire") ?? 7200;
        var algorithm = section["Algorithm"] ?? "HS256";

        var jwt = new JwtBuilder
        {
            Issuer = issuer,
            Algorithm = algorithm,
            Secret = secret,
            Subject = loginName,
            Expire = DateTime.Now.AddSeconds(expire),
        };

        return jwt.Encode(null);
    }
}
