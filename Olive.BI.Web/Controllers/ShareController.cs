using Microsoft.AspNetCore.Mvc;
using NewLife;
using NewLife.Data;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;
using XCode;

namespace Olive.BI.Web.Controllers;

/// <summary>报表分享</summary>
[ApiController]
[Route("share")]
public class ShareController : EntityController
{
    private readonly IConfiguration _config;

    /// <summary>构造</summary>
    /// <param name="config">配置</param>
    public ShareController(IConfiguration config) => _config = config;

    /// <summary>创建分享</summary>
    public class CreateShareRequest
    {
        /// <summary>报表编码</summary>
        public String? ReportCode { get; set; }

        /// <summary>有效期类型：0=永久 1=小时 2=天 3=周 4=自定义</summary>
        public Int32 ShareValidType { get; set; }

        /// <summary>自定义有效期截止时间（仅 custom 使用）</summary>
        public DateTime? ShareValidTime { get; set; }

        /// <summary>访问密码（可选）</summary>
        public String? SharePassword { get; set; }
    }

    /// <summary>创建分享</summary>
    /// <param name="req">创建请求</param>
    /// <returns>分享实体</returns>
    [HttpPost("create")]
    public Result<ReportShareEntity> Create([FromBody] CreateShareRequest req)
    {
        if (req == null || req.ReportCode.IsNullOrEmpty())
            return new Result<ReportShareEntity> { State = 1, Msg = "报表编码必填" };

        var validType = req.ShareValidType;
        var validTime = validType switch
        {
            1 => DateTime.Now.AddHours(1),
            2 => DateTime.Now.AddDays(1),
            3 => DateTime.Now.AddDays(7),
            4 => req.ShareValidTime ?? DateTime.Now.AddDays(7),
            _ => new DateTime(2099, 12, 31),
        };

        var entity = new ReportShare
        {
            ReportCode = req.ReportCode,
            ShareCode = Guid.NewGuid().ToString("N"),
            ShareToken = Guid.NewGuid().ToString("N"),
            ShareValidType = validType,
            ShareValidTime = validTime,
            SharePassword = req.SharePassword,
            EnableFlag = 1,
        };

        var baseUrl = _config["Share:BaseUrl"] ?? "/share";
        entity.ShareUrl = $"{baseUrl.TrimEnd('/')}/{entity.ShareCode}";

        entity.Insert();
        return ResultHelper.ToResult(entity.ToModel());
    }

    /// <summary>按报表编码列出分享</summary>
    /// <param name="req">报表编码请求</param>
    /// <returns>分享列表</returns>
    [HttpPost("listByReport")]
    public Result<List<ReportShareEntity>> ListByReport([FromBody] DashboardController.ReportCodeRequest req)
    {
        if (req == null || req.ReportCode.IsNullOrEmpty())
            return new Result<List<ReportShareEntity>> { State = 1, Msg = "报表编码必填" };

        var list = ReportShare.FindAll(ReportShare._.ReportCode == req.ReportCode & ReportShare._.DeleteFlag == 0,
            ReportShare._.Id.Desc(), null, 0, 0);
        return ResultHelper.ToResult(list.Select(e => e.ToModel()).ToList());
    }

    /// <summary>删除分享</summary>
    /// <param name="req">Id请求</param>
    /// <returns>结果</returns>
    [HttpPost("delete")]
    public Result Delete([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };
        var entity = ReportShare.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "分享不存在" };
        entity.DeleteFlag = 1;
        entity.Update();
        return new Result().ToResult();
    }

    /// <summary>验证分享访问</summary>
    public class VerifyRequest
    {
        /// <summary>分享编码</summary>
        public String? ShareCode { get; set; }

        /// <summary>访问密码</summary>
        public String? Password { get; set; }
    }

    /// <summary>分享验证响应</summary>
    public class VerifyResponse
    {
        /// <summary>报表编码</summary>
        public String? ReportCode { get; set; }

        /// <summary>临时令牌（用于后续API鉴权）</summary>
        public String? Token { get; set; }

        /// <summary>是否需要密码</summary>
        public Boolean NeedPassword { get; set; }
    }

    /// <summary>验证分享访问权限</summary>
    /// <param name="req">验证请求</param>
    /// <returns>验证结果</returns>
    [HttpPost("verify")]
    public Result<VerifyResponse> Verify([FromBody] VerifyRequest req)
    {
        if (req == null || req.ShareCode.IsNullOrEmpty())
            return new Result<VerifyResponse> { State = 1, Msg = "分享编码必填" };

        var entity = ReportShare.FindByShareCode(req.ShareCode!);
        if (entity == null || entity.DeleteFlag == 1 || entity.EnableFlag != 1)
            return new Result<VerifyResponse> { State = 1, Msg = "分享不存在或已禁用" };

        if (entity.ShareValidTime < DateTime.Now && entity.ShareValidType != 0)
            return new Result<VerifyResponse> { State = 1, Msg = "分享已过期" };

        // 密码校验
        if (!entity.SharePassword.IsNullOrEmpty())
        {
            if (req.Password.IsNullOrEmpty())
                return ResultHelper.ToResult(new VerifyResponse { NeedPassword = true });
            if (req.Password != entity.SharePassword)
                return new Result<VerifyResponse> { State = 1, Msg = "访问密码错误" };
        }

        return ResultHelper.ToResult(new VerifyResponse
        {
            ReportCode = entity.ReportCode,
            Token = entity.ShareToken,
            NeedPassword = false,
        });
    }
}
