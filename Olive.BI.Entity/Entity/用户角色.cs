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

/// <summary>用户角色。用户与角色关联</summary>
[Serializable]
[DataObject]
[Description("用户角色。用户与角色关联")]
[BindIndex("IX_UserRole_LoginName", false, "LoginName")]
[BindIndex("IU_UserRole_LoginName_RoleCode", true, "LoginName,RoleCode")]
[BindTable("UserRole", Description = "用户角色。用户与角色关联", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class UserRole : IEntity<UserRoleEntity>
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
    [DataObjectField(false, false, false, 32)]
    [BindColumn("LoginName", "登录名", "")]
    public String LoginName { get => _LoginName; set { if (OnPropertyChanging("LoginName", value)) { _LoginName = value; OnPropertyChanged("LoginName"); } } }

    private String _RoleCode = null!;
    /// <summary>角色编码</summary>
    [DisplayName("角色编码")]
    [Description("角色编码")]
    [DataObjectField(false, false, false, 32)]
    [BindColumn("RoleCode", "角色编码", "")]
    public String RoleCode { get => _RoleCode; set { if (OnPropertyChanging("RoleCode", value)) { _RoleCode = value; OnPropertyChanged("RoleCode"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(UserRoleEntity model)
    {
        Id = model.Id;
        LoginName = model.LoginName;
        RoleCode = model.RoleCode;
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
            "RoleCode" => _RoleCode,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "LoginName": _LoginName = Convert.ToString(value); break;
                case "RoleCode": _RoleCode = Convert.ToString(value); break;
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
    public static UserRole? FindById(Int32 id)
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
    /// <returns>实体列表</returns>
    public static IList<UserRole> FindAllByLoginName(String loginName)
    {
        if (loginName.IsNullOrEmpty()) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.LoginName.EqualIgnoreCase(loginName));

        return FindAll(_.LoginName == loginName);
    }

    /// <summary>根据登录名、角色编码查找</summary>
    /// <param name="loginName">登录名</param>
    /// <param name="roleCode">角色编码</param>
    /// <returns>实体对象</returns>
    public static UserRole? FindByLoginNameAndRoleCode(String loginName, String roleCode)
    {
        if (loginName.IsNullOrEmpty()) return null;
        if (roleCode.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.LoginName.EqualIgnoreCase(loginName) && e.RoleCode.EqualIgnoreCase(roleCode));

        return Find(_.LoginName == loginName & _.RoleCode == roleCode);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="loginName">登录名</param>
    /// <param name="roleCode">角色编码</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<UserRole> Search(String loginName, String roleCode, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!loginName.IsNullOrEmpty()) exp &= _.LoginName == loginName;
        if (!roleCode.IsNullOrEmpty()) exp &= _.RoleCode == roleCode;
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得用户角色字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>登录名</summary>
        public static readonly Field LoginName = FindByName("LoginName");

        /// <summary>角色编码</summary>
        public static readonly Field RoleCode = FindByName("RoleCode");

        static Field FindByName(String name) => Meta.Table.FindByName(name)!;
    }

    /// <summary>取得用户角色字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>登录名</summary>
        public const String LoginName = "LoginName";

        /// <summary>角色编码</summary>
        public const String RoleCode = "RoleCode";
    }
    #endregion
}
