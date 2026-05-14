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

/// <summary>报表分享。报表分享配置</summary>
[Serializable]
[DataObject]
[Description("报表分享。报表分享配置")]
[BindIndex("IU_ReportShare_ShareCode", true, "ShareCode")]
[BindIndex("IX_ReportShare_ReportCode", false, "ReportCode")]
[BindTable("ReportShare", Description = "报表分享。报表分享配置", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class ReportShare : IEntity<ReportShareEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String? _ShareCode;
    /// <summary>分享编码。UUID</summary>
    [DisplayName("分享编码")]
    [Description("分享编码。UUID")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("ShareCode", "分享编码。UUID", "", Master = true)]
    public String? ShareCode { get => _ShareCode; set { if (OnPropertyChanging("ShareCode", value)) { _ShareCode = value; OnPropertyChanged("ShareCode"); } } }

    private Int32 _ShareValidType;
    /// <summary>分享有效期类型。字典SHARE_VAILD</summary>
    [DisplayName("分享有效期类型")]
    [Description("分享有效期类型。字典SHARE_VAILD")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ShareValidType", "分享有效期类型。字典SHARE_VAILD", "")]
    public Int32 ShareValidType { get => _ShareValidType; set { if (OnPropertyChanging("ShareValidType", value)) { _ShareValidType = value; OnPropertyChanged("ShareValidType"); } } }

    private DateTime _ShareValidTime;
    /// <summary>分享有效期</summary>
    [DisplayName("分享有效期")]
    [Description("分享有效期")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("ShareValidTime", "分享有效期", "")]
    public DateTime ShareValidTime { get => _ShareValidTime; set { if (OnPropertyChanging("ShareValidTime", value)) { _ShareValidTime = value; OnPropertyChanged("ShareValidTime"); } } }

    private String? _ShareToken;
    /// <summary>分享Token</summary>
    [DisplayName("分享Token")]
    [Description("分享Token")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("ShareToken", "分享Token", "")]
    public String? ShareToken { get => _ShareToken; set { if (OnPropertyChanging("ShareToken", value)) { _ShareToken = value; OnPropertyChanged("ShareToken"); } } }

    private String? _ShareUrl;
    /// <summary>分享URL</summary>
    [DisplayName("分享URL")]
    [Description("分享URL")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("ShareUrl", "分享URL", "")]
    public String? ShareUrl { get => _ShareUrl; set { if (OnPropertyChanging("ShareUrl", value)) { _ShareUrl = value; OnPropertyChanged("ShareUrl"); } } }

    private String? _SharePassword;
    /// <summary>分享密码</summary>
    [DisplayName("分享密码")]
    [Description("分享密码")]
    [DataObjectField(false, false, true, 10)]
    [BindColumn("SharePassword", "分享密码", "")]
    public String? SharePassword { get => _SharePassword; set { if (OnPropertyChanging("SharePassword", value)) { _SharePassword = value; OnPropertyChanged("SharePassword"); } } }

    private String? _ReportCode;
    /// <summary>报表编码</summary>
    [DisplayName("报表编码")]
    [Description("报表编码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("ReportCode", "报表编码", "")]
    public String? ReportCode { get => _ReportCode; set { if (OnPropertyChanging("ReportCode", value)) { _ReportCode = value; OnPropertyChanged("ReportCode"); } } }

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
    public void Copy(ReportShareEntity model)
    {
        Id = model.Id;
        ShareCode = model.ShareCode;
        ShareValidType = model.ShareValidType;
        ShareValidTime = model.ShareValidTime;
        ShareToken = model.ShareToken;
        ShareUrl = model.ShareUrl;
        SharePassword = model.SharePassword;
        ReportCode = model.ReportCode;
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
            "ShareCode" => _ShareCode,
            "ShareValidType" => _ShareValidType,
            "ShareValidTime" => _ShareValidTime,
            "ShareToken" => _ShareToken,
            "ShareUrl" => _ShareUrl,
            "SharePassword" => _SharePassword,
            "ReportCode" => _ReportCode,
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
                case "ShareCode": _ShareCode = Convert.ToString(value); break;
                case "ShareValidType": _ShareValidType = value.ToInt(); break;
                case "ShareValidTime": _ShareValidTime = value.ToDateTime(); break;
                case "ShareToken": _ShareToken = Convert.ToString(value); break;
                case "ShareUrl": _ShareUrl = Convert.ToString(value); break;
                case "SharePassword": _SharePassword = Convert.ToString(value); break;
                case "ReportCode": _ReportCode = Convert.ToString(value); break;
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
    public static ReportShare? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据分享编码查找</summary>
    /// <param name="shareCode">分享编码</param>
    /// <returns>实体对象</returns>
    public static ReportShare? FindByShareCode(String? shareCode)
    {
        if (shareCode == null) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ShareCode.EqualIgnoreCase(shareCode));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(shareCode) as ReportShare;

        //return Find(_.ShareCode == shareCode);
    }

    /// <summary>根据报表编码查找</summary>
    /// <param name="reportCode">报表编码</param>
    /// <returns>实体列表</returns>
    public static IList<ReportShare> FindAllByReportCode(String? reportCode)
    {
        if (reportCode == null) return [];

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
    public static IList<ReportShare> Search(String? reportCode, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!reportCode.IsNullOrEmpty()) exp &= _.ReportCode == reportCode;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得报表分享字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>分享编码。UUID</summary>
        public static readonly Field ShareCode = FindByName("ShareCode");

        /// <summary>分享有效期类型。字典SHARE_VAILD</summary>
        public static readonly Field ShareValidType = FindByName("ShareValidType");

        /// <summary>分享有效期</summary>
        public static readonly Field ShareValidTime = FindByName("ShareValidTime");

        /// <summary>分享Token</summary>
        public static readonly Field ShareToken = FindByName("ShareToken");

        /// <summary>分享URL</summary>
        public static readonly Field ShareUrl = FindByName("ShareUrl");

        /// <summary>分享密码</summary>
        public static readonly Field SharePassword = FindByName("SharePassword");

        /// <summary>报表编码</summary>
        public static readonly Field ReportCode = FindByName("ReportCode");

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

    /// <summary>取得报表分享字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>分享编码。UUID</summary>
        public const String ShareCode = "ShareCode";

        /// <summary>分享有效期类型。字典SHARE_VAILD</summary>
        public const String ShareValidType = "ShareValidType";

        /// <summary>分享有效期</summary>
        public const String ShareValidTime = "ShareValidTime";

        /// <summary>分享Token</summary>
        public const String ShareToken = "ShareToken";

        /// <summary>分享URL</summary>
        public const String ShareUrl = "ShareUrl";

        /// <summary>分享密码</summary>
        public const String SharePassword = "SharePassword";

        /// <summary>报表编码</summary>
        public const String ReportCode = "ReportCode";

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
