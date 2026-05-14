using NewLife;
using NewLife.Data;
using XCode;

namespace Olive.BI.Entity;

/// <summary>角色。业务扩展（手写部分）</summary>
public partial class Role
{
    #region 业务方法

    /// <summary>获取角色拥有的所有权限</summary>
    /// <returns>角色权限列表</returns>
    public IList<RoleAuthority> GetAuthorities()
    {
        if (RoleCode.IsNullOrEmpty()) return [];

        return RoleAuthority.FindAll(RoleAuthority._.RoleCode == RoleCode);
    }

    /// <summary>分页查询角色</summary>
    /// <param name="keywords">关键字：角色编码/角色名称</param>
    /// <param name="enableFlag">启用标记。null表示不过滤</param>
    /// <param name="page">分页参数</param>
    /// <returns>角色列表</returns>
    public static IList<Role> SearchPage(String? keywords, Int32? enableFlag, PageParameter page)
    {
        var exp = new WhereExpression();
        exp &= _.DeleteFlag == 0;

        if (!keywords.IsNullOrEmpty())
            exp &= _.RoleCode.Contains(keywords) | _.RoleName.Contains(keywords);

        if (enableFlag != null) exp &= _.EnableFlag == enableFlag.Value;

        return FindAll(exp, page);
    }

    #endregion
}
