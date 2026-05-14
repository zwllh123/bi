using NewLife;
using NewLife.Data;
using XCode;

namespace Olive.BI.Entity;

/// <summary>权限菜单树节点</summary>
public class AuthorityNode
{
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>父菜单代码</summary>
    public String? ParentTarget { get; set; }

    /// <summary>菜单代码</summary>
    public String Target { get; set; } = null!;

    /// <summary>菜单名称</summary>
    public String? TargetName { get; set; }

    /// <summary>按钮代码</summary>
    public String? Action { get; set; }

    /// <summary>按钮名称</summary>
    public String? ActionName { get; set; }

    /// <summary>排序</summary>
    public Int32 Sort { get; set; }

    /// <summary>子节点</summary>
    public List<AuthorityNode> Children { get; set; } = [];
}

/// <summary>权限菜单。业务扩展（手写部分）</summary>
public partial class Authority
{
    #region 业务方法

    /// <summary>查询全部启用权限</summary>
    /// <returns>权限列表</returns>
    public static IList<Authority> FindAllEnabled()
        => FindAll(_.DeleteFlag == 0 & _.EnableFlag == 1, _.Sort.Asc(), null, 0, 0);

    /// <summary>构建权限树。按 ParentTarget/Target 组装</summary>
    /// <returns>根节点列表</returns>
    public static List<AuthorityNode> GetTree()
    {
        var list = FindAllEnabled();
        var nodes = list.Select(e => new AuthorityNode
        {
            Id = e.Id,
            ParentTarget = e.ParentTarget,
            Target = e.Target,
            TargetName = e.TargetName,
            Action = e.Action,
            ActionName = e.ActionName,
            Sort = e.Sort,
        }).ToList();

        var map = nodes.GroupBy(n => n.Target).ToDictionary(g => g.Key, g => g.First());
        var roots = new List<AuthorityNode>();

        foreach (var n in nodes)
        {
            if (!n.ParentTarget.IsNullOrEmpty() && map.TryGetValue(n.ParentTarget!, out var parent) && parent != n)
                parent.Children.Add(n);
            else
                roots.Add(n);
        }

        return roots;
    }

    /// <summary>分页查询权限</summary>
    /// <param name="keywords">关键字：菜单代码/菜单名称/按钮代码</param>
    /// <param name="enableFlag">启用标记</param>
    /// <param name="page">分页参数</param>
    /// <returns>权限列表</returns>
    public static IList<Authority> SearchPage(String? keywords, Int32? enableFlag, PageParameter page)
    {
        var exp = new WhereExpression();
        exp &= _.DeleteFlag == 0;

        if (!keywords.IsNullOrEmpty())
            exp &= _.Target.Contains(keywords) | _.TargetName.Contains(keywords) | _.Action.Contains(keywords);

        if (enableFlag != null) exp &= _.EnableFlag == enableFlag.Value;

        return FindAll(exp, page);
    }

    #endregion
}
