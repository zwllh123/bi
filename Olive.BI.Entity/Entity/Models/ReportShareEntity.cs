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

/// <summary>报表分享。报表分享配置</summary>
public partial class ReportShareEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>分享编码。UUID</summary>
    public String? ShareCode { get; set; }

    /// <summary>分享有效期类型。字典SHARE_VAILD</summary>
    public Int32 ShareValidType { get; set; }

    /// <summary>分享有效期</summary>
    public DateTime ShareValidTime { get; set; }

    /// <summary>分享Token</summary>
    public String? ShareToken { get; set; }

    /// <summary>分享URL</summary>
    public String? ShareUrl { get; set; }

    /// <summary>分享密码</summary>
    public String? SharePassword { get; set; }

    /// <summary>报表编码</summary>
    public String? ReportCode { get; set; }

    /// <summary>启用标记</summary>
    public Int32 EnableFlag { get; set; }

    /// <summary>删除标记</summary>
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
                "ShareCode" => ShareCode,
                "ShareValidType" => ShareValidType,
                "ShareValidTime" => ShareValidTime,
                "ShareToken" => ShareToken,
                "ShareUrl" => ShareUrl,
                "SharePassword" => SharePassword,
                "ReportCode" => ReportCode,
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
                case "ShareCode": ShareCode = Convert.ToString(value); break;
                case "ShareValidType": ShareValidType = value.ToInt(); break;
                case "ShareValidTime": ShareValidTime = value.ToDateTime(); break;
                case "ShareToken": ShareToken = Convert.ToString(value); break;
                case "ShareUrl": ShareUrl = Convert.ToString(value); break;
                case "SharePassword": SharePassword = Convert.ToString(value); break;
                case "ReportCode": ReportCode = Convert.ToString(value); break;
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
    public void Copy(ReportShareEntity model)
    {
        Id = model.Id;
        ShareCode = model.ShareCode;
        ShareValidType = model.ShareValidType;
        ShareValidTime = model.ShareValidTime;
        ShareToken = model.ShareToken;
        ShareUrl = model.ShareUrl;
        SharePassword = model.SharePassword;
        ReportCode = model.ReportCode;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion
}
