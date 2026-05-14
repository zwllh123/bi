using NewLife;
using NewLife.Data;
using XCode;

namespace Olive.BI.Entity;

/// <summary>字典项。业务扩展</summary>
public partial class DictItem
{
    #region 业务方法

    /// <summary>分页搜索</summary>
    /// <param name="dictCode">字典编码</param>
    /// <param name="keywords">关键字：项名/项值</param>
    /// <param name="enabled">启用过滤：null 不过滤</param>
    /// <param name="page">分页参数</param>
    /// <returns>字典项分页</returns>
    public static IList<DictItem> SearchPage(String? dictCode, String? keywords, Int32? enabled, PageParameter page)
    {
        var exp = new WhereExpression();
        if (!dictCode.IsNullOrEmpty()) exp &= _.DictCode == dictCode;
        if (!keywords.IsNullOrEmpty())
            exp &= _.ItemName.Contains(keywords) | _.ItemValue.Contains(keywords);
        if (enabled != null) exp &= _.Enabled == enabled.Value;

        return FindAll(exp, page);
    }

    #endregion
}
