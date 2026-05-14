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

/// <summary>数据源。数据源管理</summary>
public partial class ReportDataSourceEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>数据源编码</summary>
    public String? SourceCode { get; set; }

    /// <summary>数据源名称</summary>
    public String? SourceName { get; set; }

    /// <summary>数据源描述</summary>
    public String? SourceDesc { get; set; }

    /// <summary>数据源类型。字典SOURCE_TYPE</summary>
    public String? SourceType { get; set; }

    /// <summary>连接配置json</summary>
    public String? SourceConfig { get; set; }

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
                "SourceCode" => SourceCode,
                "SourceName" => SourceName,
                "SourceDesc" => SourceDesc,
                "SourceType" => SourceType,
                "SourceConfig" => SourceConfig,
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
                case "SourceCode": SourceCode = Convert.ToString(value); break;
                case "SourceName": SourceName = Convert.ToString(value); break;
                case "SourceDesc": SourceDesc = Convert.ToString(value); break;
                case "SourceType": SourceType = Convert.ToString(value); break;
                case "SourceConfig": SourceConfig = Convert.ToString(value); break;
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
    public void Copy(ReportDataSourceEntity model)
    {
        Id = model.Id;
        SourceCode = model.SourceCode;
        SourceName = model.SourceName;
        SourceDesc = model.SourceDesc;
        SourceType = model.SourceType;
        SourceConfig = model.SourceConfig;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion
}
