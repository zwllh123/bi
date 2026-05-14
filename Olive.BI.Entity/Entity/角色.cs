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

/// <summary>角色。运营角色表</summary>
[Serializable]
[DataObject]
[Description("角色。运营角色表")]
[BindIndex("IU_Role_RoleCode", true, "RoleCode")]
[BindTable("Role", Description = "角色。运营角色表", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class Role : IEntity<RoleEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _RoleCode = null!;
    /// <summary>角色编码</summary>
    [DisplayName("角色编码")]
    [Description("角色编码")]
    [DataObjectField(false, false, false, 32)]
    [BindColumn("RoleCode", "角色编码", "", Master = true)]
    public String RoleCode { get => _RoleCode; set { if (OnPropertyChanging("RoleCode", value)) { _RoleCode = value; OnPropertyChanged("RoleCode"); } } }

    private String _RoleName = null!;
    /// <summary>角色名称</summary>
    [DisplayName("角色名称")]
    [Description("角色名称")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("RoleName", "角色名称", "")]
    public String RoleName { get => _RoleName; set { if (OnPropertyChanging("RoleName", value)) { _RoleName = value; OnPropertyChanged("RoleName"); } } }

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
    public void Copy(RoleEntity model)
    {
        Id = model.Id;
        RoleCode = model.RoleCode;
        RoleName = model.RoleName;
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
            "RoleCode" => _RoleCode,
            "RoleName" => _RoleName,
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
                case "RoleCode": _RoleCode = Convert.ToString(value); break;
                case "RoleName": _RoleName = Convert.ToString(value); break;
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
    public static Role? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据角色编码查找</summary>
    /// <param name="roleCode">角色编码</param>
    /// <returns>实体对象</returns>
    public static Role? FindByRoleCode(String roleCode)
    {
        if (roleCode.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.RoleCode.EqualIgnoreCase(roleCode));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(roleCode) as Role;

        //return Find(_.RoleCode == roleCode);
    }
    #endregion

    #region 字段名
    /// <summary>取得角色字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>角色编码</summary>
        public static readonly Field RoleCode = FindByName("RoleCode");

        /// <summary>角色名称</summary>
        public static readonly Field RoleName = FindByName("RoleName");

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

    /// <summary>取得角色字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>角色编码</summary>
        public const String RoleCode = "RoleCode";

        /// <summary>角色名称</summary>
        public const String RoleName = "RoleName";

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
