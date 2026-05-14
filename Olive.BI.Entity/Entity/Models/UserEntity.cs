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

/// <summary>用户。运营用户表</summary>
public partial class UserEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>登录名</summary>
    public String LoginName { get; set; } = null!;

    /// <summary>真实姓名</summary>
    public String RealName { get; set; } = null!;

    /// <summary>密码。32位MD5</summary>
    public String Password { get; set; } = null!;

    /// <summary>手机号码</summary>
    public String? Phone { get; set; }

    /// <summary>邮箱</summary>
    public String? Email { get; set; }

    /// <summary>备注</summary>
    public String? Remark { get; set; }

    /// <summary>最后登录时间</summary>
    public DateTime LastLoginTime { get; set; }

    /// <summary>最后登录IP</summary>
    public String? LastLoginIP { get; set; }

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
                "LoginName" => LoginName,
                "RealName" => RealName,
                "Password" => Password,
                "Phone" => Phone,
                "Email" => Email,
                "Remark" => Remark,
                "LastLoginTime" => LastLoginTime,
                "LastLoginIP" => LastLoginIP,
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
                case "LoginName": LoginName = Convert.ToString(value); break;
                case "RealName": RealName = Convert.ToString(value); break;
                case "Password": Password = Convert.ToString(value); break;
                case "Phone": Phone = Convert.ToString(value); break;
                case "Email": Email = Convert.ToString(value); break;
                case "Remark": Remark = Convert.ToString(value); break;
                case "LastLoginTime": LastLoginTime = value.ToDateTime(); break;
                case "LastLoginIP": LastLoginIP = Convert.ToString(value); break;
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
}
