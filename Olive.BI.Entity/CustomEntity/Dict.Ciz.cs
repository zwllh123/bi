using NewLife;
using NewLife.Data;
using XCode;

namespace Olive.BI.Entity;

/// <summary>数据字典。业务扩展</summary>
public partial class Dict
{
    #region 业务方法

    /// <summary>分页搜索</summary>
    /// <param name="keywords">关键字：字典编码/名称</param>
    /// <param name="page">分页参数</param>
    /// <returns>字典分页</returns>
    public static IList<Dict> SearchPage(String? keywords, PageParameter page)
    {
        var exp = new WhereExpression();
        if (!keywords.IsNullOrEmpty())
            exp &= _.DictCode.Contains(keywords) | _.DictName.Contains(keywords);

        return FindAll(exp, page);
    }

    #endregion
}
