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

/// <summary>Excel报表。Excel类型报表配置</summary>
public partial class ReportExcelEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>报表编码</summary>
    public String? ReportCode { get; set; }

    /// <summary>数据集编码列表，以|分割</summary>
    public String? SetCodes { get; set; }

    /// <summary>数据集查询参数</summary>
    public String? SetParam { get; set; }

    /// <summary>报表json串</summary>
    public String? JsonStr { get; set; }

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
                "ReportCode" => ReportCode,
                "SetCodes" => SetCodes,
                "SetParam" => SetParam,
                "JsonStr" => JsonStr,
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
                case "ReportCode": ReportCode = Convert.ToString(value); break;
                case "SetCodes": SetCodes = Convert.ToString(value); break;
                case "SetParam": SetParam = Convert.ToString(value); break;
                case "JsonStr": JsonStr = Convert.ToString(value); break;
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
    public void Copy(ReportExcelEntity model)
    {
        Id = model.Id;
        ReportCode = model.ReportCode;
        SetCodes = model.SetCodes;
        SetParam = model.SetParam;
        JsonStr = model.JsonStr;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion
}
