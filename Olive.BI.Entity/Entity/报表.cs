using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Olive.BI.Entity;

/// <summary>报表。报表主表</summary>
[Serializable]
[DataObject]
[Description("报表。报表主表")]
[BindIndex("IU_Report_ReportCode", true, "ReportCode")]
[BindIndex("IX_Report_ReportGroup", false, "ReportGroup")]
[BindIndex("IX_Report_ReportType", false, "ReportType")]
[BindTable("Report", Description = "报表。报表主表", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class Report : IEntity<ReportEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String? _ReportName;
    /// <summary>报表名称</summary>
    [DisplayName("报表名称")]
    [Description("报表名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("ReportName", "报表名称", "", Master = true)]
    public String? ReportName { get => _ReportName; set { if (OnPropertyChanging("ReportName", value)) { _ReportName = value; OnPropertyChanged("ReportName"); } } }

    private String? _ReportCode;
    /// <summary>报表编码</summary>
    [DisplayName("报表编码")]
    [Description("报表编码")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("ReportCode", "报表编码", "")]
    public String? ReportCode { get => _ReportCode; set { if (OnPropertyChanging("ReportCode", value)) { _ReportCode = value; OnPropertyChanged("ReportCode"); } } }

    private String? _ReportGroup;
    /// <summary>报表分组</summary>
    [DisplayName("报表分组")]
    [Description("报表分组")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("ReportGroup", "报表分组", "")]
    public String? ReportGroup { get => _ReportGroup; set { if (OnPropertyChanging("ReportGroup", value)) { _ReportGroup = value; OnPropertyChanged("ReportGroup"); } } }

    private String? _ReportType;
    /// <summary>报表类型。report_screen/report_excel</summary>
    [DisplayName("报表类型")]
    [Description("报表类型。report_screen/report_excel")]
    [DataObjectField(false, false, true, 20)]
    [BindColumn("ReportType", "报表类型。report_screen/report_excel", "")]
    public String? ReportType { get => _ReportType; set { if (OnPropertyChanging("ReportType", value)) { _ReportType = value; OnPropertyChanged("ReportType"); } } }

    private String? _ReportImage;
    /// <summary>报表缩略图</summary>
    [DisplayName("报表缩略图")]
    [Description("报表缩略图")]
    [DataObjectField(false, false, true, 512)]
    [BindColumn("ReportImage", "报表缩略图", "")]
    public String? ReportImage { get => _ReportImage; set { if (OnPropertyChanging("ReportImage", value)) { _ReportImage = value; OnPropertyChanged("ReportImage"); } } }

    private String? _ReportDesc;
    /// <summary>报表描述</summary>
    [DisplayName("报表描述")]
    [Description("报表描述")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("ReportDesc", "报表描述", "")]
    public String? ReportDesc { get => _ReportDesc; set { if (OnPropertyChanging("ReportDesc", value)) { _ReportDesc = value; OnPropertyChanged("ReportDesc"); } } }

    private String? _ReportAuthor;
    /// <summary>报表作者</summary>
    [DisplayName("报表作者")]
    [Description("报表作者")]
    [DataObjectField(false, false, true, 512)]
    [BindColumn("ReportAuthor", "报表作者", "")]
    public String? ReportAuthor { get => _ReportAuthor; set { if (OnPropertyChanging("ReportAuthor", value)) { _ReportAuthor = value; OnPropertyChanged("ReportAuthor"); } } }

    private Int64 _DownloadCount;
    /// <summary>下载次数</summary>
    [DisplayName("下载次数")]
    [Description("下载次数")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("DownloadCount", "下载次数", "")]
    public Int64 DownloadCount { get => _DownloadCount; set { if (OnPropertyChanging("DownloadCount", value)) { _DownloadCount = value; OnPropertyChanged("DownloadCount"); } } }

    private Int32 _EnableFlag;
    /// <summary>启用标记。0-禁用 1-启用</summary>
    [DisplayName("启用标记")]
    [Description("启用标记。0-禁用 1-启用")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("EnableFlag", "启用标记。0-禁用 1-启用", "", DefaultValue = "1")]
    public Int32 EnableFlag { get => _EnableFlag; set { if (OnPropertyChanging("EnableFlag", value)) { _EnableFlag = value; OnPropertyChanged("EnableFlag"); } } }

    private Int32 _DeleteFlag;
    /// <summary>删除标记。0-未删除 1-已删除</summary>
    [DisplayName("删除标记")]
    [Description("删除标记。0-未删除 1-已删除")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("DeleteFlag", "删除标记。0-未删除 1-已删除", "", DefaultValue = "0")]
    public Int32 DeleteFlag { get => _DeleteFlag; set { if (OnPropertyChanging("DeleteFlag", value)) { _DeleteFlag = value; OnPropertyChanged("DeleteFlag"); } } }

    private String? _CreateUser;
    /// <summary>创建者</summary>
    [Category("扩展")]
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("CreateUser", "创建者", "")]
    public String? CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [Category("扩展")]
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String? _UpdateUser;
    /// <summary>更新者</summary>
    [Category("扩展")]
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("UpdateUser", "更新者", "")]
    public String? UpdateUser { get => _UpdateUser; set { if (OnPropertyChanging("UpdateUser", value)) { _UpdateUser = value; OnPropertyChanged("UpdateUser"); } } }

    private DateTime _UpdateTime;
    /// <summary>更新时间</summary>
    [Category("扩展")]
    [DisplayName("更新时间")]
    [Description("更新时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("UpdateTime", "更新时间", "")]
    public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

    private Int32 _Version;
    /// <summary>版本号</summary>
    [Category("扩展")]
    [DisplayName("版本号")]
    [Description("版本号")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Version", "版本号", "")]
    public Int32 Version { get => _Version; set { if (OnPropertyChanging("Version", value)) { _Version = value; OnPropertyChanged("Version"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ReportEntity model)
    {
        Id = model.Id;
        ReportName = model.ReportName;
        ReportCode = model.ReportCode;
        ReportGroup = model.ReportGroup;
        ReportType = model.ReportType;
        ReportImage = model.ReportImage;
        ReportDesc = model.ReportDesc;
        ReportAuthor = model.ReportAuthor;
        DownloadCount = model.DownloadCount;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object? this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "ReportName" => _ReportName,
            "ReportCode" => _ReportCode,
            "ReportGroup" => _ReportGroup,
            "ReportType" => _ReportType,
            "ReportImage" => _ReportImage,
            "ReportDesc" => _ReportDesc,
            "ReportAuthor" => _ReportAuthor,
            "DownloadCount" => _DownloadCount,
            "EnableFlag" => _EnableFlag,
            "DeleteFlag" => _DeleteFlag,
            "CreateUser" => _CreateUser,
            "CreateTime" => _CreateTime,
            "UpdateUser" => _UpdateUser,
            "UpdateTime" => _UpdateTime,
            "Version" => _Version,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "ReportName": _ReportName = Convert.ToString(value); break;
                case "ReportCode": _ReportCode = Convert.ToString(value); break;
                case "ReportGroup": _ReportGroup = Convert.ToString(value); break;
                case "ReportType": _ReportType = Convert.ToString(value); break;
                case "ReportImage": _ReportImage = Convert.ToString(value); break;
                case "ReportDesc": _ReportDesc = Convert.ToString(value); break;
                case "ReportAuthor": _ReportAuthor = Convert.ToString(value); break;
                case "DownloadCount": _DownloadCount = value.ToLong(); break;
                case "EnableFlag": _EnableFlag = value.ToInt(); break;
                case "DeleteFlag": _DeleteFlag = value.ToInt(); break;
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "UpdateUser": _UpdateUser = Convert.ToString(value); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                case "Version": _Version = value.ToInt(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static Report? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据报表编码查找</summary>
    /// <param name="reportCode">报表编码</param>
    /// <returns>实体对象</returns>
    public static Report? FindByReportCode(String? reportCode)
    {
        if (reportCode == null) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ReportCode.EqualIgnoreCase(reportCode));

        return Find(_.ReportCode == reportCode);
    }

    /// <summary>根据报表分组查找</summary>
    /// <param name="reportGroup">报表分组</param>
    /// <returns>实体列表</returns>
    public static IList<Report> FindAllByReportGroup(String? reportGroup)
    {
        if (reportGroup == null) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ReportGroup.EqualIgnoreCase(reportGroup));

        return FindAll(_.ReportGroup == reportGroup);
    }

    /// <summary>根据报表类型查找</summary>
    /// <param name="reportType">报表类型</param>
    /// <returns>实体列表</returns>
    public static IList<Report> FindAllByReportType(String? reportType)
    {
        if (reportType == null) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ReportType.EqualIgnoreCase(reportType));

        return FindAll(_.ReportType == reportType);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="reportCode">报表编码</param>
    /// <param name="reportGroup">报表分组</param>
    /// <param name="reportType">报表类型。report_screen/report_excel</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<Report> Search(String? reportCode, String? reportGroup, String? reportType, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!reportCode.IsNullOrEmpty()) exp &= _.ReportCode == reportCode;
        if (!reportGroup.IsNullOrEmpty()) exp &= _.ReportGroup == reportGroup;
        if (!reportType.IsNullOrEmpty()) exp &= _.ReportType == reportType;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得报表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>报表名称</summary>
        public static readonly Field ReportName = FindByName("ReportName");

        /// <summary>报表编码</summary>
        public static readonly Field ReportCode = FindByName("ReportCode");

        /// <summary>报表分组</summary>
        public static readonly Field ReportGroup = FindByName("ReportGroup");

        /// <summary>报表类型。report_screen/report_excel</summary>
        public static readonly Field ReportType = FindByName("ReportType");

        /// <summary>报表缩略图</summary>
        public static readonly Field ReportImage = FindByName("ReportImage");

        /// <summary>报表描述</summary>
        public static readonly Field ReportDesc = FindByName("ReportDesc");

        /// <summary>报表作者</summary>
        public static readonly Field ReportAuthor = FindByName("ReportAuthor");

        /// <summary>下载次数</summary>
        public static readonly Field DownloadCount = FindByName("DownloadCount");

        /// <summary>启用标记。0-禁用 1-启用</summary>
        public static readonly Field EnableFlag = FindByName("EnableFlag");

        /// <summary>删除标记。0-未删除 1-已删除</summary>
        public static readonly Field DeleteFlag = FindByName("DeleteFlag");

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUser = FindByName("UpdateUser");

        /// <summary>更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

        /// <summary>版本号</summary>
        public static readonly Field Version = FindByName("Version");

        static Field FindByName(String name) => Meta.Table.FindByName(name)!;
    }

    /// <summary>取得报表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>报表名称</summary>
        public const String ReportName = "ReportName";

        /// <summary>报表编码</summary>
        public const String ReportCode = "ReportCode";

        /// <summary>报表分组</summary>
        public const String ReportGroup = "ReportGroup";

        /// <summary>报表类型。report_screen/report_excel</summary>
        public const String ReportType = "ReportType";

        /// <summary>报表缩略图</summary>
        public const String ReportImage = "ReportImage";

        /// <summary>报表描述</summary>
        public const String ReportDesc = "ReportDesc";

        /// <summary>报表作者</summary>
        public const String ReportAuthor = "ReportAuthor";

        /// <summary>下载次数</summary>
        public const String DownloadCount = "DownloadCount";

        /// <summary>启用标记。0-禁用 1-启用</summary>
        public const String EnableFlag = "EnableFlag";

        /// <summary>删除标记。0-未删除 1-已删除</summary>
        public const String DeleteFlag = "DeleteFlag";

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>更新者</summary>
        public const String UpdateUser = "UpdateUser";

        /// <summary>更新时间</summary>
        public const String UpdateTime = "UpdateTime";

        /// <summary>版本号</summary>
        public const String Version = "Version";
    }
    #endregion
}
