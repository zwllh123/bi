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

/// <summary>大屏组件。大屏中的组件配置</summary>
public partial class ReportDashboardWidgetEntity : IModel
{
    #region 属性
    /// <summary>组件编号</summary>
    public Int32 Id { get; set; }

    /// <summary>报表编码</summary>
    public String ReportCode { get; set; } = null!;

    /// <summary>组件类型。字典DASHBOARD_PANEL_TYPE</summary>
    public String? Type { get; set; }

    /// <summary>渲染属性json</summary>
    public String? Setup { get; set; }

    /// <summary>数据属性json</summary>
    public String? Data { get; set; }

    /// <summary>配置属性json</summary>
    public String? Collapse { get; set; }

    /// <summary>大小位置json</summary>
    public String? Position { get; set; }

    /// <summary>options配置项</summary>
    public String? Options { get; set; }

    /// <summary>自动刷新间隔秒</summary>
    public Int32 RefreshSeconds { get; set; }

    /// <summary>启用标记</summary>
    public Int32 EnableFlag { get; set; }

    /// <summary>删除标记</summary>
    public Int32 DeleteFlag { get; set; }

    /// <summary>排序（图层）</summary>
    public Int64 Sort { get; set; }
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
                "Type" => Type,
                "Setup" => Setup,
                "Data" => Data,
                "Collapse" => Collapse,
                "Position" => Position,
                "Options" => Options,
                "RefreshSeconds" => RefreshSeconds,
                "EnableFlag" => EnableFlag,
                "DeleteFlag" => DeleteFlag,
                "Sort" => Sort,
                _ => this.GetValue(name, false),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "ReportCode": ReportCode = Convert.ToString(value); break;
                case "Type": Type = Convert.ToString(value); break;
                case "Setup": Setup = Convert.ToString(value); break;
                case "Data": Data = Convert.ToString(value); break;
                case "Collapse": Collapse = Convert.ToString(value); break;
                case "Position": Position = Convert.ToString(value); break;
                case "Options": Options = Convert.ToString(value); break;
                case "RefreshSeconds": RefreshSeconds = value.ToInt(); break;
                case "EnableFlag": EnableFlag = value.ToInt(); break;
                case "DeleteFlag": DeleteFlag = value.ToInt(); break;
                case "Sort": Sort = value.ToLong(); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ReportDashboardWidgetEntity model)
    {
        Id = model.Id;
        ReportCode = model.ReportCode;
        Type = model.Type;
        Setup = model.Setup;
        Data = model.Data;
        Collapse = model.Collapse;
        Position = model.Position;
        Options = model.Options;
        RefreshSeconds = model.RefreshSeconds;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
        Sort = model.Sort;
    }
    #endregion
}
