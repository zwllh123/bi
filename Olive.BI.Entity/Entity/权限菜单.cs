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

/// <summary>权限菜单。运营权限表，菜单/按钮树形结构</summary>
[Serializable]
[DataObject]
[Description("权限菜单。运营权限表，菜单/按钮树形结构")]
[BindIndex("IU_Authority_Target_Action", true, "Target,Action")]
[BindIndex("IX_Authority_ParentTarget", false, "ParentTarget")]
[BindTable("Authority", Description = "权限菜单。运营权限表，菜单/按钮树形结构", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class Authority : IEntity<AuthorityEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String? _ParentTarget;
    /// <summary>父菜单代码</summary>
    [DisplayName("父菜单代码")]
    [Description("父菜单代码")]
    [DataObjectField(false, false, true, 64)]
    [BindColumn("ParentTarget", "父菜单代码", "")]
    public String? ParentTarget { get => _ParentTarget; set { if (OnPropertyChanging("ParentTarget", value)) { _ParentTarget = value; OnPropertyChanged("ParentTarget"); } } }

    private String _Target = null!;
    /// <summary>菜单代码</summary>
    [DisplayName("菜单代码")]
    [Description("菜单代码")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("Target", "菜单代码", "", Master = true)]
    public String Target { get => _Target; set { if (OnPropertyChanging("Target", value)) { _Target = value; OnPropertyChanged("Target"); } } }

    private String _TargetName = null!;
    /// <summary>菜单名称</summary>
    [DisplayName("菜单名称")]
    [Description("菜单名称")]
    [DataObjectField(false, false, false, 128)]
    [BindColumn("TargetName", "菜单名称", "")]
    public String TargetName { get => _TargetName; set { if (OnPropertyChanging("TargetName", value)) { _TargetName = value; OnPropertyChanged("TargetName"); } } }

    private String _Action = null!;
    /// <summary>按钮代码</summary>
    [DisplayName("按钮代码")]
    [Description("按钮代码")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("Action", "按钮代码", "")]
    public String Action { get => _Action; set { if (OnPropertyChanging("Action", value)) { _Action = value; OnPropertyChanged("Action"); } } }

    private String _ActionName = null!;
    /// <summary>按钮名称</summary>
    [DisplayName("按钮名称")]
    [Description("按钮名称")]
    [DataObjectField(false, false, false, 128)]
    [BindColumn("ActionName", "按钮名称", "")]
    public String ActionName { get => _ActionName; set { if (OnPropertyChanging("ActionName", value)) { _ActionName = value; OnPropertyChanged("ActionName"); } } }

    private Int32 _Sort;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sort", "排序", "")]
    public Int32 Sort { get => _Sort; set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } } }

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
    public void Copy(AuthorityEntity model)
    {
        Id = model.Id;
        ParentTarget = model.ParentTarget;
        Target = model.Target;
        TargetName = model.TargetName;
        Action = model.Action;
        ActionName = model.ActionName;
        Sort = model.Sort;
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
            "ParentTarget" => _ParentTarget,
            "Target" => _Target,
            "TargetName" => _TargetName,
            "Action" => _Action,
            "ActionName" => _ActionName,
            "Sort" => _Sort,
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
                case "ParentTarget": _ParentTarget = Convert.ToString(value); break;
                case "Target": _Target = Convert.ToString(value); break;
                case "TargetName": _TargetName = Convert.ToString(value); break;
                case "Action": _Action = Convert.ToString(value); break;
                case "ActionName": _ActionName = Convert.ToString(value); break;
                case "Sort": _Sort = value.ToInt(); break;
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
    public static Authority? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据菜单代码、按钮代码查找</summary>
    /// <param name="target">菜单代码</param>
    /// <param name="action">按钮代码</param>
    /// <returns>实体对象</returns>
    public static Authority? FindByTargetAndAction(String target, String action)
    {
        if (target.IsNullOrEmpty()) return null;
        if (action.IsNullOrEmpty()) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Target.EqualIgnoreCase(target) && e.Action.EqualIgnoreCase(action));

        return Find(_.Target == target & _.Action == action);
    }

    /// <summary>根据菜单代码查找</summary>
    /// <param name="target">菜单代码</param>
    /// <returns>实体列表</returns>
    public static IList<Authority> FindAllByTarget(String target)
    {
        if (target.IsNullOrEmpty()) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Target.EqualIgnoreCase(target));

        return FindAll(_.Target == target);
    }

    /// <summary>根据父菜单代码查找</summary>
    /// <param name="parentTarget">父菜单代码</param>
    /// <returns>实体列表</returns>
    public static IList<Authority> FindAllByParentTarget(String? parentTarget)
    {
        if (parentTarget == null) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ParentTarget.EqualIgnoreCase(parentTarget));

        return FindAll(_.ParentTarget == parentTarget);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="parentTarget">父菜单代码</param>
    /// <param name="action">按钮代码</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<Authority> Search(String? parentTarget, String action, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!parentTarget.IsNullOrEmpty()) exp &= _.ParentTarget == parentTarget;
        if (!action.IsNullOrEmpty()) exp &= _.Action == action;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得权限菜单字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>父菜单代码</summary>
        public static readonly Field ParentTarget = FindByName("ParentTarget");

        /// <summary>菜单代码</summary>
        public static readonly Field Target = FindByName("Target");

        /// <summary>菜单名称</summary>
        public static readonly Field TargetName = FindByName("TargetName");

        /// <summary>按钮代码</summary>
        public static readonly Field Action = FindByName("Action");

        /// <summary>按钮名称</summary>
        public static readonly Field ActionName = FindByName("ActionName");

        /// <summary>排序</summary>
        public static readonly Field Sort = FindByName("Sort");

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

    /// <summary>取得权限菜单字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>父菜单代码</summary>
        public const String ParentTarget = "ParentTarget";

        /// <summary>菜单代码</summary>
        public const String Target = "Target";

        /// <summary>菜单名称</summary>
        public const String TargetName = "TargetName";

        /// <summary>按钮代码</summary>
        public const String Action = "Action";

        /// <summary>按钮名称</summary>
        public const String ActionName = "ActionName";

        /// <summary>排序</summary>
        public const String Sort = "Sort";

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
