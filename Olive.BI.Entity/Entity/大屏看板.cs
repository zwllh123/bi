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

/// <summary>大屏看板。大屏报表配置</summary>
[Serializable]
[DataObject]
[Description("大屏看板。大屏报表配置")]
[BindIndex("IU_ReportDashboard_ReportCode", true, "ReportCode")]
[BindTable("ReportDashboard", Description = "大屏看板。大屏报表配置", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class ReportDashboard : IEntity<ReportDashboardEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>看板编号</summary>
    [DisplayName("看板编号")]
    [Description("看板编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "看板编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _ReportCode = null!;
    /// <summary>报表编码</summary>
    [DisplayName("报表编码")]
    [Description("报表编码")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("ReportCode", "报表编码", "", Master = true)]
    public String ReportCode { get => _ReportCode; set { if (OnPropertyChanging("ReportCode", value)) { _ReportCode = value; OnPropertyChanged("ReportCode"); } } }

    private String? _Title;
    /// <summary>看板标题</summary>
    [DisplayName("看板标题")]
    [Description("看板标题")]
    [DataObjectField(false, false, true, 254)]
    [BindColumn("Title", "看板标题", "")]
    public String? Title { get => _Title; set { if (OnPropertyChanging("Title", value)) { _Title = value; OnPropertyChanged("Title"); } } }

    private Int64 _Width;
    /// <summary>画布宽度px</summary>
    [DisplayName("画布宽度px")]
    [Description("画布宽度px")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Width", "画布宽度px", "")]
    public Int64 Width { get => _Width; set { if (OnPropertyChanging("Width", value)) { _Width = value; OnPropertyChanged("Width"); } } }

    private Int64 _Height;
    /// <summary>画布高度px</summary>
    [DisplayName("画布高度px")]
    [Description("画布高度px")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Height", "画布高度px", "")]
    public Int64 Height { get => _Height; set { if (OnPropertyChanging("Height", value)) { _Height = value; OnPropertyChanged("Height"); } } }

    private String? _BackgroundColor;
    /// <summary>背景颜色</summary>
    [DisplayName("背景颜色")]
    [Description("背景颜色")]
    [DataObjectField(false, false, true, 24)]
    [BindColumn("BackgroundColor", "背景颜色", "")]
    public String? BackgroundColor { get => _BackgroundColor; set { if (OnPropertyChanging("BackgroundColor", value)) { _BackgroundColor = value; OnPropertyChanged("BackgroundColor"); } } }

    private String? _BackgroundImage;
    /// <summary>背景图片</summary>
    [DisplayName("背景图片")]
    [Description("背景图片")]
    [DataObjectField(false, false, true, 254)]
    [BindColumn("BackgroundImage", "背景图片", "")]
    public String? BackgroundImage { get => _BackgroundImage; set { if (OnPropertyChanging("BackgroundImage", value)) { _BackgroundImage = value; OnPropertyChanged("BackgroundImage"); } } }

    private String? _PresetLine;
    /// <summary>工作台辅助线</summary>
    [DisplayName("工作台辅助线")]
    [Description("工作台辅助线")]
    [DataObjectField(false, false, true, 4096)]
    [BindColumn("PresetLine", "工作台辅助线", "")]
    public String? PresetLine { get => _PresetLine; set { if (OnPropertyChanging("PresetLine", value)) { _PresetLine = value; OnPropertyChanged("PresetLine"); } } }

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

    private Int32 _Sort;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sort", "排序", "", DefaultValue = "0")]
    public Int32 Sort { get => _Sort; set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } } }

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
    public void Copy(ReportDashboardEntity model)
    {
        Id = model.Id;
        ReportCode = model.ReportCode;
        Title = model.Title;
        Width = model.Width;
        Height = model.Height;
        BackgroundColor = model.BackgroundColor;
        BackgroundImage = model.BackgroundImage;
        PresetLine = model.PresetLine;
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
            "Title" => _Title,
            "Width" => _Width,
            "Height" => _Height,
            "BackgroundColor" => _BackgroundColor,
            "BackgroundImage" => _BackgroundImage,
            "PresetLine" => _PresetLine,
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
                case "Title": _Title = Convert.ToString(value); break;
                case "Width": _Width = value.ToLong(); break;
                case "Height": _Height = value.ToLong(); break;
                case "BackgroundColor": _BackgroundColor = Convert.ToString(value); break;
                case "BackgroundImage": _BackgroundImage = Convert.ToString(value); break;
                case "PresetLine": _PresetLine = Convert.ToString(value); break;
                case "RefreshSeconds": _RefreshSeconds = value.ToInt(); break;
                case "EnableFlag": _EnableFlag = value.ToInt(); break;
                case "DeleteFlag": _DeleteFlag = value.ToInt(); break;
                case "Sort": _Sort = value.ToInt(); break;
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
    /// <summary>根据看板编号查找</summary>
    /// <param name="id">看板编号</param>
    /// <returns>实体对象</returns>
    public static ReportDashboard? FindById(Int32 id)
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
    public static ReportDashboard? FindByReportCode(String reportCode)
    {
        if (reportCode.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ReportCode.EqualIgnoreCase(reportCode));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(reportCode) as ReportDashboard;

        //return Find(_.ReportCode == reportCode);
    }
    #endregion

    #region 字段名
    /// <summary>取得大屏看板字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>看板编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>报表编码</summary>
        public static readonly Field ReportCode = FindByName("ReportCode");

        /// <summary>看板标题</summary>
        public static readonly Field Title = FindByName("Title");

        /// <summary>画布宽度px</summary>
        public static readonly Field Width = FindByName("Width");

        /// <summary>画布高度px</summary>
        public static readonly Field Height = FindByName("Height");

        /// <summary>背景颜色</summary>
        public static readonly Field BackgroundColor = FindByName("BackgroundColor");

        /// <summary>背景图片</summary>
        public static readonly Field BackgroundImage = FindByName("BackgroundImage");

        /// <summary>工作台辅助线</summary>
        public static readonly Field PresetLine = FindByName("PresetLine");

        /// <summary>自动刷新间隔秒</summary>
        public static readonly Field RefreshSeconds = FindByName("RefreshSeconds");

        /// <summary>启用标记</summary>
        public static readonly Field EnableFlag = FindByName("EnableFlag");

        /// <summary>删除标记</summary>
        public static readonly Field DeleteFlag = FindByName("DeleteFlag");

        /// <summary>排序</summary>
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

    /// <summary>取得大屏看板字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>看板编号</summary>
        public const String Id = "Id";

        /// <summary>报表编码</summary>
        public const String ReportCode = "ReportCode";

        /// <summary>看板标题</summary>
        public const String Title = "Title";

        /// <summary>画布宽度px</summary>
        public const String Width = "Width";

        /// <summary>画布高度px</summary>
        public const String Height = "Height";

        /// <summary>背景颜色</summary>
        public const String BackgroundColor = "BackgroundColor";

        /// <summary>背景图片</summary>
        public const String BackgroundImage = "BackgroundImage";

        /// <summary>工作台辅助线</summary>
        public const String PresetLine = "PresetLine";

        /// <summary>自动刷新间隔秒</summary>
        public const String RefreshSeconds = "RefreshSeconds";

        /// <summary>启用标记</summary>
        public const String EnableFlag = "EnableFlag";

        /// <summary>删除标记</summary>
        public const String DeleteFlag = "DeleteFlag";

        /// <summary>排序</summary>
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
