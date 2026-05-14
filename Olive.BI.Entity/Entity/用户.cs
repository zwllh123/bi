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

/// <summary>用户。运营用户表</summary>
[Serializable]
[DataObject]
[Description("用户。运营用户表")]
[BindIndex("IU_User_LoginName", true, "LoginName")]
[BindTable("User", Description = "用户。运营用户表", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class User : IEntity<UserEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _LoginName = null!;
    /// <summary>登录名</summary>
    [DisplayName("登录名")]
    [Description("登录名")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("LoginName", "登录名", "", Master = true)]
    public String LoginName { get => _LoginName; set { if (OnPropertyChanging("LoginName", value)) { _LoginName = value; OnPropertyChanged("LoginName"); } } }

    private String _RealName = null!;
    /// <summary>真实姓名</summary>
    [DisplayName("真实姓名")]
    [Description("真实姓名")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("RealName", "真实姓名", "")]
    public String RealName { get => _RealName; set { if (OnPropertyChanging("RealName", value)) { _RealName = value; OnPropertyChanged("RealName"); } } }

    private String _Password = null!;
    /// <summary>密码。32位MD5</summary>
    [DisplayName("密码")]
    [Description("密码。32位MD5")]
    [DataObjectField(false, false, false, 128)]
    [BindColumn("Password", "密码。32位MD5", "")]
    public String Password { get => _Password; set { if (OnPropertyChanging("Password", value)) { _Password = value; OnPropertyChanged("Password"); } } }

    private String? _Phone;
    /// <summary>手机号码</summary>
    [DisplayName("手机号码")]
    [Description("手机号码")]
    [DataObjectField(false, false, true, 16)]
    [BindColumn("Phone", "手机号码", "")]
    public String? Phone { get => _Phone; set { if (OnPropertyChanging("Phone", value)) { _Phone = value; OnPropertyChanged("Phone"); } } }

    private String? _Email;
    /// <summary>邮箱</summary>
    [DisplayName("邮箱")]
    [Description("邮箱")]
    [DataObjectField(false, false, true, 64)]
    [BindColumn("Email", "邮箱", "")]
    public String? Email { get => _Email; set { if (OnPropertyChanging("Email", value)) { _Email = value; OnPropertyChanged("Email"); } } }

    private String? _Remark;
    /// <summary>备注</summary>
    [DisplayName("备注")]
    [Description("备注")]
    [DataObjectField(false, false, true, 512)]
    [BindColumn("Remark", "备注", "")]
    public String? Remark { get => _Remark; set { if (OnPropertyChanging("Remark", value)) { _Remark = value; OnPropertyChanged("Remark"); } } }

    private DateTime _LastLoginTime;
    /// <summary>最后登录时间</summary>
    [DisplayName("最后登录时间")]
    [Description("最后登录时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("LastLoginTime", "最后登录时间", "")]
    public DateTime LastLoginTime { get => _LastLoginTime; set { if (OnPropertyChanging("LastLoginTime", value)) { _LastLoginTime = value; OnPropertyChanged("LastLoginTime"); } } }

    private String? _LastLoginIP;
    /// <summary>最后登录IP</summary>
    [DisplayName("最后登录IP")]
    [Description("最后登录IP")]
    [DataObjectField(false, false, true, 16)]
    [BindColumn("LastLoginIP", "最后登录IP", "")]
    public String? LastLoginIP { get => _LastLoginIP; set { if (OnPropertyChanging("LastLoginIP", value)) { _LastLoginIP = value; OnPropertyChanged("LastLoginIP"); } } }

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
    public void Copy(UserEntity model)
    {
        Id = model.Id;
        LoginName = model.LoginName;
        RealName = model.RealName;
        Password = model.Password;
        Phone = model.Phone;
        Email = model.Email;
        Remark = model.Remark;
        LastLoginTime = model.LastLoginTime;
        LastLoginIP = model.LastLoginIP;
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
            "LoginName" => _LoginName,
            "RealName" => _RealName,
            "Password" => _Password,
            "Phone" => _Phone,
            "Email" => _Email,
            "Remark" => _Remark,
            "LastLoginTime" => _LastLoginTime,
            "LastLoginIP" => _LastLoginIP,
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
                case "LoginName": _LoginName = Convert.ToString(value); break;
                case "RealName": _RealName = Convert.ToString(value); break;
                case "Password": _Password = Convert.ToString(value); break;
                case "Phone": _Phone = Convert.ToString(value); break;
                case "Email": _Email = Convert.ToString(value); break;
                case "Remark": _Remark = Convert.ToString(value); break;
                case "LastLoginTime": _LastLoginTime = value.ToDateTime(); break;
                case "LastLoginIP": _LastLoginIP = Convert.ToString(value); break;
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
    public static User? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据登录名查找</summary>
    /// <param name="loginName">登录名</param>
    /// <returns>实体对象</returns>
    public static User? FindByLoginName(String loginName)
    {
        if (loginName.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.LoginName.EqualIgnoreCase(loginName));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(loginName) as User;

        //return Find(_.LoginName == loginName);
    }
    #endregion

    #region 字段名
    /// <summary>取得用户字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>登录名</summary>
        public static readonly Field LoginName = FindByName("LoginName");

        /// <summary>真实姓名</summary>
        public static readonly Field RealName = FindByName("RealName");

        /// <summary>密码。32位MD5</summary>
        public static readonly Field Password = FindByName("Password");

        /// <summary>手机号码</summary>
        public static readonly Field Phone = FindByName("Phone");

        /// <summary>邮箱</summary>
        public static readonly Field Email = FindByName("Email");

        /// <summary>备注</summary>
        public static readonly Field Remark = FindByName("Remark");

        /// <summary>最后登录时间</summary>
        public static readonly Field LastLoginTime = FindByName("LastLoginTime");

        /// <summary>最后登录IP</summary>
        public static readonly Field LastLoginIP = FindByName("LastLoginIP");

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

    /// <summary>取得用户字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>登录名</summary>
        public const String LoginName = "LoginName";

        /// <summary>真实姓名</summary>
        public const String RealName = "RealName";

        /// <summary>密码。32位MD5</summary>
        public const String Password = "Password";

        /// <summary>手机号码</summary>
        public const String Phone = "Phone";

        /// <summary>邮箱</summary>
        public const String Email = "Email";

        /// <summary>备注</summary>
        public const String Remark = "Remark";

        /// <summary>最后登录时间</summary>
        public const String LastLoginTime = "LastLoginTime";

        /// <summary>最后登录IP</summary>
        public const String LastLoginIP = "LastLoginIP";

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
