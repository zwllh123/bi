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

/// <summary>数据集参数。数据集查询参数</summary>
public partial class ReportDataSetParamEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>数据集编码</summary>
    public String? SetCode { get; set; }

    /// <summary>参数名</summary>
    public String? ParamName { get; set; }

    /// <summary>参数描述</summary>
    public String? ParamDesc { get; set; }

    /// <summary>参数类型</summary>
    public String? ParamType { get; set; }

    /// <summary>参数示例项</summary>
    public String? SampleItem { get; set; }

    /// <summary>是否必填。0-否 1-是</summary>
    public Int32 RequiredFlag { get; set; }

    /// <summary>JS校验规则</summary>
    public String? ValidationRules { get; set; }

    /// <summary>排序</summary>
    public Int32 OrderNum { get; set; }

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
                "SetCode" => SetCode,
                "ParamName" => ParamName,
                "ParamDesc" => ParamDesc,
                "ParamType" => ParamType,
                "SampleItem" => SampleItem,
                "RequiredFlag" => RequiredFlag,
                "ValidationRules" => ValidationRules,
                "OrderNum" => OrderNum,
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
                case "SetCode": SetCode = Convert.ToString(value); break;
                case "ParamName": ParamName = Convert.ToString(value); break;
                case "ParamDesc": ParamDesc = Convert.ToString(value); break;
                case "ParamType": ParamType = Convert.ToString(value); break;
                case "SampleItem": SampleItem = Convert.ToString(value); break;
                case "RequiredFlag": RequiredFlag = value.ToInt(); break;
                case "ValidationRules": ValidationRules = Convert.ToString(value); break;
                case "OrderNum": OrderNum = value.ToInt(); break;
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
    public void Copy(ReportDataSetParamEntity model)
    {
        Id = model.Id;
        SetCode = model.SetCode;
        ParamName = model.ParamName;
        ParamDesc = model.ParamDesc;
        ParamType = model.ParamType;
        SampleItem = model.SampleItem;
        RequiredFlag = model.RequiredFlag;
        ValidationRules = model.ValidationRules;
        OrderNum = model.OrderNum;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion
}
