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

/// <summary>Excel报表。Excel类型报表配置</summary>
[Serializable]
[DataObject]
[Description("Excel报表。Excel类型报表配置")]
[BindIndex("IU_ReportExcel_ReportCode", true, "ReportCode")]
[BindTable("ReportExcel", Description = "Excel报表。Excel类型报表配置", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class ReportExcel : IEntity<ReportExcelEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String? _ReportCode;
    /// <summary>报表编码</summary>
    [DisplayName("报表编码")]
    [Description("报表编码")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("ReportCode", "报表编码", "", Master = true)]
    public String? ReportCode { get => _ReportCode; set { if (OnPropertyChanging("ReportCode", value)) { _ReportCode = value; OnPropertyChanged("ReportCode"); } } }

    private String? _SetCodes;
    /// <summary>数据集编码列表，以|分割</summary>
    [DisplayName("数据集编码列表")]
    [Description("数据集编码列表，以|分割")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("SetCodes", "数据集编码列表，以|分割", "")]
    public String? SetCodes { get => _SetCodes; set { if (OnPropertyChanging("SetCodes", value)) { _SetCodes = value; OnPropertyChanged("SetCodes"); } } }

    private String? _SetParam;
    /// <summary>数据集查询参数</summary>
    [DisplayName("数据集查询参数")]
    [Description("数据集查询参数")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("SetParam", "数据集查询参数", "")]
    public String? SetParam { get => _SetParam; set { if (OnPropertyChanging("SetParam", value)) { _SetParam = value; OnPropertyChanged("SetParam"); } } }

    private String? _JsonStr;
    /// <summary>报表json串</summary>
    [DisplayName("报表json串")]
    [Description("报表json串")]
    [DataObjectField(false, false, true, -1)]
    [BindColumn("JsonStr", "报表json串", "")]
    public String? JsonStr { get => _JsonStr; set { if (OnPropertyChanging("JsonStr", value)) { _JsonStr = value; OnPropertyChanged("JsonStr"); } } }

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
    public void Copy(ReportExcelEntity model)
    {
        Id = model.Id;
        ReportCode = model.ReportCode;
        SetCodes = model.SetCodes;
        SetParam = model.SetParam;
        JsonStr = model.JsonStr;
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
            "ReportCode" => _ReportCode,
            "SetCodes" => _SetCodes,
            "SetParam" => _SetParam,
            "JsonStr" => _JsonStr,
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
                case "ReportCode": _ReportCode = Convert.ToString(value); break;
                case "SetCodes": _SetCodes = Convert.ToString(value); break;
                case "SetParam": _SetParam = Convert.ToString(value); break;
                case "JsonStr": _JsonStr = Convert.ToString(value); break;
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
    public static ReportExcel? FindById(Int32 id)
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
    public static ReportExcel? FindByReportCode(String? reportCode)
    {
        if (reportCode == null) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ReportCode.EqualIgnoreCase(reportCode));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(reportCode) as ReportExcel;

        //return Find(_.ReportCode == reportCode);
    }
    #endregion

    #region 字段名
    /// <summary>取得Excel报表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>报表编码</summary>
        public static readonly Field ReportCode = FindByName("ReportCode");

        /// <summary>数据集编码列表，以|分割</summary>
        public static readonly Field SetCodes = FindByName("SetCodes");

        /// <summary>数据集查询参数</summary>
        public static readonly Field SetParam = FindByName("SetParam");

        /// <summary>报表json串</summary>
        public static readonly Field JsonStr = FindByName("JsonStr");

        /// <summary>启用标记</summary>
        public static readonly Field EnableFlag = FindByName("EnableFlag");

        /// <summary>删除标记</summary>
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

    /// <summary>取得Excel报表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>报表编码</summary>
        public const String ReportCode = "ReportCode";

        /// <summary>数据集编码列表，以|分割</summary>
        public const String SetCodes = "SetCodes";

        /// <summary>数据集查询参数</summary>
        public const String SetParam = "SetParam";

        /// <summary>报表json串</summary>
        public const String JsonStr = "JsonStr";

        /// <summary>启用标记</summary>
        public const String EnableFlag = "EnableFlag";

        /// <summary>删除标记</summary>
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
