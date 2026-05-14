using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Reflection;

namespace Olive.BI.Entity;

/// <summary>权限菜单。运营权限表，菜单/按钮树形结构</summary>
public partial class AuthorityEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>父菜单代码</summary>
    public String? ParentTarget { get; set; }

    /// <summary>菜单代码</summary>
    public String Target { get; set; } = null!;

    /// <summary>菜单名称</summary>
    public String TargetName { get; set; } = null!;

    /// <summary>按钮代码</summary>
    public String Action { get; set; } = null!;

    /// <summary>按钮名称</summary>
    public String ActionName { get; set; } = null!;

    /// <summary>排序</summary>
    public Int32 Sort { get; set; }

    /// <summary>启用标记。0-禁用 1-启用</summary>
    public Int32 EnableFlag { get; set; }

    /// <summary>删除标记。0-未删除 1-已删除</summary>
    public Int32 DeleteFlag { get; set; }
    #endregion

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public virtual Object? this[String name]
    {
        get
        {
            return name switch
            {
                "Id" => Id,
                "ParentTarget" => ParentTarget,
                "Target" => Target,
                "TargetName" => TargetName,
                "Action" => Action,
                "ActionName" => ActionName,
                "Sort" => Sort,
                "EnableFlag" => EnableFlag,
                "DeleteFlag" => DeleteFlag,
                _ => this.GetValue(name, false),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "ParentTarget": ParentTarget = Convert.ToString(value); break;
                case "Target": Target = Convert.ToString(value); break;
                case "TargetName": TargetName = Convert.ToString(value); break;
                case "Action": Action = Convert.ToString(value); break;
                case "ActionName": ActionName = Convert.ToString(value); break;
                case "Sort": Sort = value.ToInt(); break;
                case "EnableFlag": EnableFlag = value.ToInt(); break;
                case "DeleteFlag": DeleteFlag = value.ToInt(); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
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
}
