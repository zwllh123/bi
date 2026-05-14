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

/// <summary>数据集转换。数据集结果转换规则</summary>
public partial class ReportDataSetTransformEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>数据集编码</summary>
    public String? SetCode { get; set; }

    /// <summary>转换类型。js/javaBean/dict</summary>
    public String? TransformType { get; set; }

    /// <summary>转换脚本/处理逻辑</summary>
    public String? TransformScript { get; set; }

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
                "TransformType" => TransformType,
                "TransformScript" => TransformScript,
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
                case "TransformType": TransformType = Convert.ToString(value); break;
                case "TransformScript": TransformScript = Convert.ToString(value); break;
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
    public void Copy(ReportDataSetTransformEntity model)
    {
        Id = model.Id;
        SetCode = model.SetCode;
        TransformType = model.TransformType;
        TransformScript = model.TransformScript;
        OrderNum = model.OrderNum;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion
}
