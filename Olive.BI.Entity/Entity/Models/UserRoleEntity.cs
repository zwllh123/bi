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

/// <summary>用户角色。用户与角色关联</summary>
public partial class UserRoleEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>登录名</summary>
    public String LoginName { get; set; } = null!;

    /// <summary>角色编码</summary>
    public String RoleCode { get; set; } = null!;
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
                "LoginName" => LoginName,
                "RoleCode" => RoleCode,
                _ => this.GetValue(name, false),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "LoginName": LoginName = Convert.ToString(value); break;
                case "RoleCode": RoleCode = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
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
}
