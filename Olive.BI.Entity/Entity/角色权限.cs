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

/// <summary>角色权限。角色与权限关联</summary>
[Serializable]
[DataObject]
[Description("角色权限。角色与权限关联")]
[BindIndex("IX_RoleAuthority_RoleCode", false, "RoleCode")]
[BindIndex("IU_RoleAuthority_RoleCode_Target_Action", true, "RoleCode,Target,Action")]
[BindTable("RoleAuthority", Description = "角色权限。角色与权限关联", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class RoleAuthority : IEntity<RoleAuthorityEntity>
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
    [BindColumn("RoleCode", "角色编码", "")]
    public String RoleCode { get => _RoleCode; set { if (OnPropertyChanging("RoleCode", value)) { _RoleCode = value; OnPropertyChanged("RoleCode"); } } }

    private String _Target = null!;
    /// <summary>权限目标</summary>
    [DisplayName("权限目标")]
    [Description("权限目标")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("Target", "权限目标", "")]
    public String Target { get => _Target; set { if (OnPropertyChanging("Target", value)) { _Target = value; OnPropertyChanged("Target"); } } }

    private String _Action = null!;
    /// <summary>权限动作</summary>
    [DisplayName("权限动作")]
    [Description("权限动作")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("Action", "权限动作", "")]
    public String Action { get => _Action; set { if (OnPropertyChanging("Action", value)) { _Action = value; OnPropertyChanged("Action"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(RoleAuthorityEntity model)
    {
        Id = model.Id;
        RoleCode = model.RoleCode;
        Target = model.Target;
        Action = model.Action;
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
            "Target" => _Target,
            "Action" => _Action,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "RoleCode": _RoleCode = Convert.ToString(value); break;
                case "Target": _Target = Convert.ToString(value); break;
                case "Action": _Action = Convert.ToString(value); break;
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
    public static RoleAuthority? FindById(Int32 id)
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
    /// <returns>实体列表</returns>
    public static IList<RoleAuthority> FindAllByRoleCode(String roleCode)
    {
        if (roleCode.IsNullOrEmpty()) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.RoleCode.EqualIgnoreCase(roleCode));

        return FindAll(_.RoleCode == roleCode);
    }

    /// <summary>根据角色编码、权限目标、权限动作查找</summary>
    /// <param name="roleCode">角色编码</param>
    /// <param name="target">权限目标</param>
    /// <param name="action">权限动作</param>
    /// <returns>实体对象</returns>
    public static RoleAuthority? FindByRoleCodeAndTargetAndAction(String roleCode, String target, String action)
    {
        if (roleCode.IsNullOrEmpty()) return null;
        if (target.IsNullOrEmpty()) return null;
        if (action.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.RoleCode.EqualIgnoreCase(roleCode) && e.Target.EqualIgnoreCase(target) && e.Action.EqualIgnoreCase(action));

        return Find(_.RoleCode == roleCode & _.Target == target & _.Action == action);
    }

    /// <summary>根据角色编码、权限目标查找</summary>
    /// <param name="roleCode">角色编码</param>
    /// <param name="target">权限目标</param>
    /// <returns>实体列表</returns>
    public static IList<RoleAuthority> FindAllByRoleCodeAndTarget(String roleCode, String target)
    {
        if (roleCode.IsNullOrEmpty()) return [];
        if (target.IsNullOrEmpty()) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.RoleCode.EqualIgnoreCase(roleCode) && e.Target.EqualIgnoreCase(target));

        return FindAll(_.RoleCode == roleCode & _.Target == target);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="roleCode">角色编码</param>
    /// <param name="target">权限目标</param>
    /// <param name="action">权限动作</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<RoleAuthority> Search(String roleCode, String target, String action, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!roleCode.IsNullOrEmpty()) exp &= _.RoleCode == roleCode;
        if (!target.IsNullOrEmpty()) exp &= _.Target == target;
        if (!action.IsNullOrEmpty()) exp &= _.Action == action;
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得角色权限字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>角色编码</summary>
        public static readonly Field RoleCode = FindByName("RoleCode");

        /// <summary>权限目标</summary>
        public static readonly Field Target = FindByName("Target");

        /// <summary>权限动作</summary>
        public static readonly Field Action = FindByName("Action");

        static Field FindByName(String name) => Meta.Table.FindByName(name)!;
    }

    /// <summary>取得角色权限字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>角色编码</summary>
        public const String RoleCode = "RoleCode";

        /// <summary>权限目标</summary>
        public const String Target = "Target";

        /// <summary>权限动作</summary>
        public const String Action = "Action";
    }
    #endregion
}
