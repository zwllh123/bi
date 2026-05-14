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

/// <summary>数据源。数据源管理</summary>
[Serializable]
[DataObject]
[Description("数据源。数据源管理")]
[BindIndex("IU_ReportDataSource_SourceCode", true, "SourceCode")]
[BindTable("ReportDataSource", Description = "数据源。数据源管理", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class ReportDataSource : IEntity<ReportDataSourceEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String? _SourceCode;
    /// <summary>数据源编码</summary>
    [DisplayName("数据源编码")]
    [Description("数据源编码")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("SourceCode", "数据源编码", "", Master = true)]
    public String? SourceCode { get => _SourceCode; set { if (OnPropertyChanging("SourceCode", value)) { _SourceCode = value; OnPropertyChanged("SourceCode"); } } }

    private String? _SourceName;
    /// <summary>数据源名称</summary>
    [DisplayName("数据源名称")]
    [Description("数据源名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("SourceName", "数据源名称", "")]
    public String? SourceName { get => _SourceName; set { if (OnPropertyChanging("SourceName", value)) { _SourceName = value; OnPropertyChanged("SourceName"); } } }

    private String? _SourceDesc;
    /// <summary>数据源描述</summary>
    [DisplayName("数据源描述")]
    [Description("数据源描述")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("SourceDesc", "数据源描述", "")]
    public String? SourceDesc { get => _SourceDesc; set { if (OnPropertyChanging("SourceDesc", value)) { _SourceDesc = value; OnPropertyChanged("SourceDesc"); } } }

    private String? _SourceType;
    /// <summary>数据源类型。字典SOURCE_TYPE</summary>
    [DisplayName("数据源类型")]
    [Description("数据源类型。字典SOURCE_TYPE")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SourceType", "数据源类型。字典SOURCE_TYPE", "")]
    public String? SourceType { get => _SourceType; set { if (OnPropertyChanging("SourceType", value)) { _SourceType = value; OnPropertyChanged("SourceType"); } } }

    private String? _SourceConfig;
    /// <summary>连接配置json</summary>
    [DisplayName("连接配置json")]
    [Description("连接配置json")]
    [DataObjectField(false, false, true, 2048)]
    [BindColumn("SourceConfig", "连接配置json", "")]
    public String? SourceConfig { get => _SourceConfig; set { if (OnPropertyChanging("SourceConfig", value)) { _SourceConfig = value; OnPropertyChanged("SourceConfig"); } } }

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
    public void Copy(ReportDataSourceEntity model)
    {
        Id = model.Id;
        SourceCode = model.SourceCode;
        SourceName = model.SourceName;
        SourceDesc = model.SourceDesc;
        SourceType = model.SourceType;
        SourceConfig = model.SourceConfig;
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
            "SourceCode" => _SourceCode,
            "SourceName" => _SourceName,
            "SourceDesc" => _SourceDesc,
            "SourceType" => _SourceType,
            "SourceConfig" => _SourceConfig,
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
                case "SourceCode": _SourceCode = Convert.ToString(value); break;
                case "SourceName": _SourceName = Convert.ToString(value); break;
                case "SourceDesc": _SourceDesc = Convert.ToString(value); break;
                case "SourceType": _SourceType = Convert.ToString(value); break;
                case "SourceConfig": _SourceConfig = Convert.ToString(value); break;
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
    public static ReportDataSource? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据数据源编码查找</summary>
    /// <param name="sourceCode">数据源编码</param>
    /// <returns>实体对象</returns>
    public static ReportDataSource? FindBySourceCode(String? sourceCode)
    {
        if (sourceCode == null) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.SourceCode.EqualIgnoreCase(sourceCode));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(sourceCode) as ReportDataSource;

        //return Find(_.SourceCode == sourceCode);
    }
    #endregion

    #region 字段名
    /// <summary>取得数据源字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>数据源编码</summary>
        public static readonly Field SourceCode = FindByName("SourceCode");

        /// <summary>数据源名称</summary>
        public static readonly Field SourceName = FindByName("SourceName");

        /// <summary>数据源描述</summary>
        public static readonly Field SourceDesc = FindByName("SourceDesc");

        /// <summary>数据源类型。字典SOURCE_TYPE</summary>
        public static readonly Field SourceType = FindByName("SourceType");

        /// <summary>连接配置json</summary>
        public static readonly Field SourceConfig = FindByName("SourceConfig");

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

    /// <summary>取得数据源字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>数据源编码</summary>
        public const String SourceCode = "SourceCode";

        /// <summary>数据源名称</summary>
        public const String SourceName = "SourceName";

        /// <summary>数据源描述</summary>
        public const String SourceDesc = "SourceDesc";

        /// <summary>数据源类型。字典SOURCE_TYPE</summary>
        public const String SourceType = "SourceType";

        /// <summary>连接配置json</summary>
        public const String SourceConfig = "SourceConfig";

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
