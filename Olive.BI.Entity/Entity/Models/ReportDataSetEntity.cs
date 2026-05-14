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

/// <summary>数据集。数据集管理</summary>
public partial class ReportDataSetEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>数据集编码</summary>
    public String? SetCode { get; set; }

    /// <summary>数据集名称</summary>
    public String? SetName { get; set; }

    /// <summary>数据集描述</summary>
    public String? SetDesc { get; set; }

    /// <summary>数据源编码</summary>
    public String? SourceCode { get; set; }

    /// <summary>动态查询SQL或请求体</summary>
    public String? DynSentence { get; set; }

    /// <summary>结果案例</summary>
    public String? CaseResult { get; set; }

    /// <summary>数据集类型。sql/http</summary>
    public String? SetType { get; set; }

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
                "SetName" => SetName,
                "SetDesc" => SetDesc,
                "SourceCode" => SourceCode,
                "DynSentence" => DynSentence,
                "CaseResult" => CaseResult,
                "SetType" => SetType,
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
                case "SetName": SetName = Convert.ToString(value); break;
                case "SetDesc": SetDesc = Convert.ToString(value); break;
                case "SourceCode": SourceCode = Convert.ToString(value); break;
                case "DynSentence": DynSentence = Convert.ToString(value); break;
                case "CaseResult": CaseResult = Convert.ToString(value); break;
                case "SetType": SetType = Convert.ToString(value); break;
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
    public void Copy(ReportDataSetEntity model)
    {
        Id = model.Id;
        SetCode = model.SetCode;
        SetName = model.SetName;
        SetDesc = model.SetDesc;
        SourceCode = model.SourceCode;
        DynSentence = model.DynSentence;
        CaseResult = model.CaseResult;
        SetType = model.SetType;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion
}
