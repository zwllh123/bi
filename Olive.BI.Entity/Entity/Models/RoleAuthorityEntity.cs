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

/// <summary>角色权限。角色与权限关联</summary>
public partial class RoleAuthorityEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>角色编码</summary>
    public String RoleCode { get; set; } = null!;

    /// <summary>权限目标</summary>
    public String Target { get; set; } = null!;

    /// <summary>权限动作</summary>
    public String Action { get; set; } = null!;
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
                "RoleCode" => RoleCode,
                "Target" => Target,
                "Action" => Action,
                _ => this.GetValue(name, false),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "RoleCode": RoleCode = Convert.ToString(value); break;
                case "Target": Target = Convert.ToString(value); break;
                case "Action": Action = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
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
}
