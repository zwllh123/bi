using NewLife;
using NewLife.Data;
using XCode;

namespace Olive.BI.Entity;

/// <summary>数据集。业务扩展</summary>
public partial class ReportDataSet
{
    /// <summary>分页搜索</summary>
    /// <param name="keywords">关键字（编码/名称）</param>
    /// <param name="sourceCode">数据源编码过滤</param>
    /// <param name="enableFlag">启用过滤</param>
    /// <param name="page">分页参数</param>
    /// <returns>数据集列表</returns>
    public static IList<ReportDataSet> SearchPage(String? keywords, String? sourceCode, Int32? enableFlag, PageParameter page)
    {
        var exp = new WhereExpression();
        exp &= _.DeleteFlag == 0;
        if (!keywords.IsNullOrEmpty())
            exp &= _.SetCode.Contains(keywords) | _.SetName.Contains(keywords);
        if (!sourceCode.IsNullOrEmpty()) exp &= _.SourceCode == sourceCode;
        if (enableFlag != null) exp &= _.EnableFlag == enableFlag.Value;
        return FindAll(exp, page);
    }

    /// <summary>取参数列表</summary>
    /// <returns>参数列表</returns>
    public IList<ReportDataSetParam> GetParams()
    {
        if (SetCode.IsNullOrEmpty()) return [];
        return ReportDataSetParam.FindAllBySetCode(SetCode!);
    }

    /// <summary>取转换列表</summary>
    /// <returns>转换列表</returns>
    public IList<ReportDataSetTransform> GetTransforms()
    {
        if (SetCode.IsNullOrEmpty()) return [];
        return ReportDataSetTransform.FindAllBySetCode(SetCode!);
    }

    /// <summary>执行数据集（SQL 类型），合并请求参数与参数定义默认值</summary>
    /// <param name="userParameters">前端传入参数</param>
    /// <param name="maxRows">最大行数</param>
    /// <returns>SQL 执行结果</returns>
    public SqlExecuteResult ExecuteSql(IDictionary<String, Object?>? userParameters, Int32 maxRows = 1000)
    {
        if (SourceCode.IsNullOrEmpty()) throw new InvalidOperationException("数据集未配置数据源");
        if (DynSentence.IsNullOrEmpty()) throw new InvalidOperationException("数据集未配置SQL");

        var src = ReportDataSource.FindBySourceCode(SourceCode!) ?? throw new InvalidOperationException("数据源不存在");
        if (src.EnableFlag != 1) throw new InvalidOperationException("数据源已禁用");

        // 合并参数默认值
        var paramsList = GetParams();
        var merged = new Dictionary<String, Object?>();
        foreach (var p in paramsList)
        {
            if (!p.ParamName.IsNullOrEmpty()) merged[p.ParamName!] = p.SampleItem;
        }
        if (userParameters != null)
        {
            foreach (var kv in userParameters) merged[kv.Key] = kv.Value;
        }

        // 必填校验
        foreach (var p in paramsList)
        {
            if (p.RequiredFlag == 1 && (!merged.TryGetValue(p.ParamName ?? "", out var v) || v == null || v.ToString().IsNullOrEmpty()))
                throw new ArgumentException($"参数 {p.ParamName} 必填");
        }

        return src.Execute(DynSentence!, merged, maxRows);
    }
}

/// <summary>数据集参数。业务扩展</summary>
public partial class ReportDataSetParam
{
    /// <summary>按数据集编码查询并排序</summary>
    /// <param name="setCode">数据集编码</param>
    /// <returns>参数列表</returns>
    public static IList<ReportDataSetParam> ListBySet(String setCode)
        => FindAllBySetCode(setCode).OrderBy(e => e.OrderNum).ToList();
}

/// <summary>数据集转换。业务扩展</summary>
public partial class ReportDataSetTransform
{
    /// <summary>按数据集编码查询并按 OrderNum 排序</summary>
    /// <param name="setCode">数据集编码</param>
    /// <returns>转换列表</returns>
    public static IList<ReportDataSetTransform> ListBySet(String setCode)
        => FindAllBySetCode(setCode).Where(e => e.DeleteFlag == 0).OrderBy(e => e.OrderNum).ToList();
}
