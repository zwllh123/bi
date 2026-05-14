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

/// <summary>角色。运营角色表</summary>
public partial class RoleEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>角色编码</summary>
    public String RoleCode { get; set; } = null!;

    /// <summary>角色名称</summary>
    public String RoleName { get; set; } = null!;

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
                "RoleCode" => RoleCode,
                "RoleName" => RoleName,
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
                case "RoleCode": RoleCode = Convert.ToString(value); break;
                case "RoleName": RoleName = Convert.ToString(value); break;
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
    public void Copy(RoleEntity model)
    {
        Id = model.Id;
        RoleCode = model.RoleCode;
        RoleName = model.RoleName;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion
}
