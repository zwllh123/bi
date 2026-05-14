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

/// <summary>大屏组件。大屏中的组件配置</summary>
[Serializable]
[DataObject]
[Description("大屏组件。大屏中的组件配置")]
[BindIndex("IX_ReportDashboardWidget_ReportCode", false, "ReportCode")]
[BindTable("ReportDashboardWidget", Description = "大屏组件。大屏中的组件配置", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class ReportDashboardWidget : IEntity<ReportDashboardWidgetEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>组件编号</summary>
    [DisplayName("组件编号")]
    [Description("组件编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "组件编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _ReportCode = null!;
    /// <summary>报表编码</summary>
    [DisplayName("报表编码")]
    [Description("报表编码")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("ReportCode", "报表编码", "")]
    public String ReportCode { get => _ReportCode; set { if (OnPropertyChanging("ReportCode", value)) { _ReportCode = value; OnPropertyChanged("ReportCode"); } } }

    private String? _Type;
    /// <summary>组件类型。字典DASHBOARD_PANEL_TYPE</summary>
    [DisplayName("组件类型")]
    [Description("组件类型。字典DASHBOARD_PANEL_TYPE")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Type", "组件类型。字典DASHBOARD_PANEL_TYPE", "")]
    public String? Type { get => _Type; set { if (OnPropertyChanging("Type", value)) { _Type = value; OnPropertyChanged("Type"); } } }

    private String? _Setup;
    /// <summary>渲染属性json</summary>
    [DisplayName("渲染属性json")]
    [Description("渲染属性json")]
    [DataObjectField(false, false, true, -1)]
    [BindColumn("Setup", "渲染属性json", "")]
    public String? Setup { get => _Setup; set { if (OnPropertyChanging("Setup", value)) { _Setup = value; OnPropertyChanged("Setup"); } } }

    private String? _Data;
    /// <summary>数据属性json</summary>
    [DisplayName("数据属性json")]
    [Description("数据属性json")]
    [DataObjectField(false, false, true, -1)]
    [BindColumn("Data", "数据属性json", "")]
    public String? Data { get => _Data; set { if (OnPropertyChanging("Data", value)) { _Data = value; OnPropertyChanged("Data"); } } }

    private String? _Collapse;
    /// <summary>配置属性json</summary>
    [DisplayName("配置属性json")]
    [Description("配置属性json")]
    [DataObjectField(false, false, true, -1)]
    [BindColumn("Collapse", "配置属性json", "")]
    public String? Collapse { get => _Collapse; set { if (OnPropertyChanging("Collapse", value)) { _Collapse = value; OnPropertyChanged("Collapse"); } } }

    private String? _Position;
    /// <summary>大小位置json</summary>
    [DisplayName("大小位置json")]
    [Description("大小位置json")]
    [DataObjectField(false, false, true, -1)]
    [BindColumn("Position", "大小位置json", "")]
    public String? Position { get => _Position; set { if (OnPropertyChanging("Position", value)) { _Position = value; OnPropertyChanged("Position"); } } }

    private String? _Options;
    /// <summary>options配置项</summary>
    [DisplayName("options配置项")]
    [Description("options配置项")]
    [DataObjectField(false, false, true, -1)]
    [BindColumn("Options", "options配置项", "")]
    public String? Options { get => _Options; set { if (OnPropertyChanging("Options", value)) { _Options = value; OnPropertyChanged("Options"); } } }

    private Int32 _RefreshSeconds;
    /// <summary>自动刷新间隔秒</summary>
    [DisplayName("自动刷新间隔秒")]
    [Description("自动刷新间隔秒")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("RefreshSeconds", "自动刷新间隔秒", "")]
    public Int32 RefreshSeconds { get => _RefreshSeconds; set { if (OnPropertyChanging("RefreshSeconds", value)) { _RefreshSeconds = value; OnPropertyChanged("RefreshSeconds"); } } }

    private Int32 _EnableFlag;
    /// <summary>启用标记</summary>
    [DisplayName("启用标记")]
    [Description("启用标记")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("EnableFlag", "启用标记", "", DefaultValue = "1")]
    public Int32 EnableFlag { get => _EnableFlag; set { if (OnPropertyChanging("EnableFlag", value)) { _EnableFlag = value; OnPropertyChanged("EnableFlag"); } } }

    private Int32 _DeleteFlag;
    /// <summary>删除标记</summary>
    [DisplayName("删除标记")]
    [Description("删除标记")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("DeleteFlag", "删除标记", "", DefaultValue = "0")]
    public Int32 DeleteFlag { get => _DeleteFlag; set { if (OnPropertyChanging("DeleteFlag", value)) { _DeleteFlag = value; OnPropertyChanged("DeleteFlag"); } } }

    private Int64 _Sort;
    /// <summary>排序（图层）</summary>
    [DisplayName("排序（图层）")]
    [Description("排序（图层）")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sort", "排序（图层）", "", DefaultValue = "0")]
    public Int64 Sort { get => _Sort; set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } } }

    private String? _CreateUser;
    /// <summary>创建者</summary>
    [Category("扩展")]
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 64)]
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
    [DataObjectField(false, false, true, 64)]
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
    public void Copy(ReportDashboardWidgetEntity model)
    {
        Id = model.Id;
        ReportCode = model.ReportCode;
        Type = model.Type;
        Setup = model.Setup;
        Data = model.Data;
        Collapse = model.Collapse;
        Position = model.Position;
        Options = model.Options;
        RefreshSeconds = model.RefreshSeconds;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
        Sort = model.Sort;
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
            "ReportCode" => _ReportCode,
            "Type" => _Type,
            "Setup" => _Setup,
            "Data" => _Data,
            "Collapse" => _Collapse,
            "Position" => _Position,
            "Options" => _Options,
            "RefreshSeconds" => _RefreshSeconds,
            "EnableFlag" => _EnableFlag,
            "DeleteFlag" => _DeleteFlag,
            "Sort" => _Sort,
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
                case "ReportCode": _ReportCode = Convert.ToString(value); break;
                case "Type": _Type = Convert.ToString(value); break;
                case "Setup": _Setup = Convert.ToString(value); break;
                case "Data": _Data = Convert.ToString(value); break;
                case "Collapse": _Collapse = Convert.ToString(value); break;
                case "Position": _Position = Convert.ToString(value); break;
                case "Options": _Options = Convert.ToString(value); break;
                case "RefreshSeconds": _RefreshSeconds = value.ToInt(); break;
                case "EnableFlag": _EnableFlag = value.ToInt(); break;
                case "DeleteFlag": _DeleteFlag = value.ToInt(); break;
                case "Sort": _Sort = value.ToLong(); break;
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
    /// <summary>根据组件编号查找</summary>
    /// <param name="id">组件编号</param>
    /// <returns>实体对象</returns>
    public static ReportDashboardWidget? FindById(Int32 id)
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
    /// <returns>实体列表</returns>
    public static IList<ReportDashboardWidget> FindAllByReportCode(String reportCode)
    {
        if (reportCode.IsNullOrEmpty()) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ReportCode.EqualIgnoreCase(reportCode));

        return FindAll(_.ReportCode == reportCode);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="reportCode">报表编码</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<ReportDashboardWidget> Search(String reportCode, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!reportCode.IsNullOrEmpty()) exp &= _.ReportCode == reportCode;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得大屏组件字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>组件编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>报表编码</summary>
        public static readonly Field ReportCode = FindByName("ReportCode");

        /// <summary>组件类型。字典DASHBOARD_PANEL_TYPE</summary>
        public static readonly Field Type = FindByName("Type");

        /// <summary>渲染属性json</summary>
        public static readonly Field Setup = FindByName("Setup");

        /// <summary>数据属性json</summary>
        public static readonly Field Data = FindByName("Data");

        /// <summary>配置属性json</summary>
        public static readonly Field Collapse = FindByName("Collapse");

        /// <summary>大小位置json</summary>
        public static readonly Field Position = FindByName("Position");

        /// <summary>options配置项</summary>
        public static readonly Field Options = FindByName("Options");

        /// <summary>自动刷新间隔秒</summary>
        public static readonly Field RefreshSeconds = FindByName("RefreshSeconds");

        /// <summary>启用标记</summary>
        public static readonly Field EnableFlag = FindByName("EnableFlag");

        /// <summary>删除标记</summary>
        public static readonly Field DeleteFlag = FindByName("DeleteFlag");

        /// <summary>排序（图层）</summary>
        public static readonly Field Sort = FindByName("Sort");

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

    /// <summary>取得大屏组件字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>组件编号</summary>
        public const String Id = "Id";

        /// <summary>报表编码</summary>
        public const String ReportCode = "ReportCode";

        /// <summary>组件类型。字典DASHBOARD_PANEL_TYPE</summary>
        public const String Type = "Type";

        /// <summary>渲染属性json</summary>
        public const String Setup = "Setup";

        /// <summary>数据属性json</summary>
        public const String Data = "Data";

        /// <summary>配置属性json</summary>
        public const String Collapse = "Collapse";

        /// <summary>大小位置json</summary>
        public const String Position = "Position";

        /// <summary>options配置项</summary>
        public const String Options = "Options";

        /// <summary>自动刷新间隔秒</summary>
        public const String RefreshSeconds = "RefreshSeconds";

        /// <summary>启用标记</summary>
        public const String EnableFlag = "EnableFlag";

        /// <summary>删除标记</summary>
        public const String DeleteFlag = "DeleteFlag";

        /// <summary>排序（图层）</summary>
        public const String Sort = "Sort";

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
