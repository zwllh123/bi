using NewLife;
using NewLife.Data;
using XCode;

namespace Olive.BI.Entity;

/// <summary>报表。业务扩展</summary>
public partial class Report
{
    /// <summary>分页搜索</summary>
    /// <param name="keywords">关键字（编码/名称）</param>
    /// <param name="reportType">报表类型过滤：report_screen/report_excel</param>
    /// <param name="reportGroup">报表分组过滤</param>
    /// <param name="enableFlag">启用过滤</param>
    /// <param name="page">分页参数</param>
    /// <returns>报表列表</returns>
    public static IList<Report> SearchPage(String? keywords, String? reportType, String? reportGroup, Int32? enableFlag, PageParameter page)
    {
        var exp = new WhereExpression();
        exp &= _.DeleteFlag == 0;
        if (!keywords.IsNullOrEmpty())
            exp &= _.ReportName.Contains(keywords) | _.ReportCode.Contains(keywords);
        if (!reportType.IsNullOrEmpty()) exp &= _.ReportType == reportType;
        if (!reportGroup.IsNullOrEmpty()) exp &= _.ReportGroup == reportGroup;
        if (enableFlag != null) exp &= _.EnableFlag == enableFlag.Value;
        return FindAll(exp, page);
    }
}

/// <summary>大屏看板。业务扩展</summary>
public partial class ReportDashboard
{
    /// <summary>按报表编码取看板（含组件）</summary>
    /// <param name="reportCode">报表编码</param>
    /// <returns>看板</returns>
    public static ReportDashboard? FindActiveByReportCode(String reportCode)
        => FindByReportCode(reportCode);

    /// <summary>取该看板的组件列表（仅未删除）</summary>
    /// <returns>组件列表</returns>
    public IList<ReportDashboardWidget> GetWidgets()
    {
        if (ReportCode.IsNullOrEmpty()) return [];
        return ReportDashboardWidget.FindAllByReportCode(ReportCode!).Where(e => e.DeleteFlag == 0).OrderBy(e => e.Sort).ToList();
    }
}

/// <summary>大屏组件。业务扩展</summary>
public partial class ReportDashboardWidget
{
    /// <summary>按报表编码取组件列表</summary>
    /// <param name="reportCode">报表编码</param>
    /// <returns>组件列表</returns>
    public static IList<ReportDashboardWidget> ListByReport(String reportCode)
        => FindAllByReportCode(reportCode).Where(e => e.DeleteFlag == 0).OrderBy(e => e.Sort).ToList();
}
