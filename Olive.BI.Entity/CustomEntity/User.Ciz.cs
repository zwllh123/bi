using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using XCode;

namespace Olive.BI.Entity;

/// <summary>用户。业务扩展（手写部分）</summary>
public partial class User
{
    #region 业务方法

    /// <summary>计算32位MD5密码哈希。兼容旧 AJ-Report 实现</summary>
    /// <param name="plain">明文密码</param>
    /// <returns>32位小写hex字符串</returns>
    public static String EncryptPassword(String plain)
    {
        if (plain == null) throw new ArgumentNullException(nameof(plain));

        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(plain));
        var sb = new StringBuilder(32);
        foreach (var b in bytes) sb.Append(b.ToString("x2"));

        return sb.ToString();
    }

    /// <summary>验证密码</summary>
    /// <param name="plain">明文密码</param>
    /// <returns>是否匹配</returns>
    public Boolean VerifyPassword(String plain)
    {
        if (plain.IsNullOrEmpty() || Password.IsNullOrEmpty()) return false;

        return String.Equals(EncryptPassword(plain), Password, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>获取用户所有角色编码</summary>
    /// <returns>角色编码列表</returns>
    public IList<String> GetRoleCodes()
    {
        if (LoginName.IsNullOrEmpty()) return [];

        var list = UserRole.FindAll(UserRole._.LoginName == LoginName);

        return list.Select(e => e.RoleCode).Where(c => !c.IsNullOrEmpty()).Distinct().ToList()!;
    }

    /// <summary>分页查询用户</summary>
    /// <param name="keywords">关键字：登录名/真实姓名</param>
    /// <param name="enableFlag">启用标记。null表示不过滤</param>
    /// <param name="page">分页参数</param>
    /// <returns>用户列表</returns>
    public static IList<User> SearchPage(String? keywords, Int32? enableFlag, PageParameter page)
    {
        var exp = new WhereExpression();
        exp &= _.DeleteFlag == 0;

        if (!keywords.IsNullOrEmpty())
            exp &= _.LoginName.Contains(keywords) | _.RealName.Contains(keywords);

        if (enableFlag != null) exp &= _.EnableFlag == enableFlag.Value;

        return FindAll(exp, page);
    }
    /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override void InitData()
    {
        // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        if (Meta.Session.Count > 0) return;

        if (XTrace.Debug) XTrace.WriteLine("开始初始化User[用户]数据……");

        var entity = new User();
        entity.LoginName = "admin";
        entity.RealName = "管理员";
        entity.Password = EncryptPassword("123456");
        entity.Phone = "abc";
        entity.Email = "abc";
        entity.LastLoginTime = DateTime.Now;
        entity.LastLoginIP = "abc";
        entity.EnableFlag = 0;
        entity.DeleteFlag = 0;
        entity.Version = 0;
        entity.Insert();

        if (XTrace.Debug) XTrace.WriteLine("完成初始化User[用户]数据！");
    }
    #endregion
}
