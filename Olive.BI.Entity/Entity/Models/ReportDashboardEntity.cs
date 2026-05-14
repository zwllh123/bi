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

/// <summary>大屏看板。大屏报表配置</summary>
public partial class ReportDashboardEntity : IModel
{
    #region 属性
    /// <summary>看板编号</summary>
    public Int32 Id { get; set; }

    /// <summary>报表编码</summary>
    public String ReportCode { get; set; } = null!;

    /// <summary>看板标题</summary>
    public String? Title { get; set; }

    /// <summary>画布宽度px</summary>
    public Int64 Width { get; set; }

    /// <summary>画布高度px</summary>
    public Int64 Height { get; set; }

    /// <summary>背景颜色</summary>
    public String? BackgroundColor { get; set; }

    /// <summary>背景图片</summary>
    public String? BackgroundImage { get; set; }

    /// <summary>工作台辅助线</summary>
    public String? PresetLine { get; set; }

    /// <summary>自动刷新间隔秒</summary>
    public Int32 RefreshSeconds { get; set; }

    /// <summary>启用标记</summary>
    public Int32 EnableFlag { get; set; }

    /// <summary>删除标记</summary>
    public Int32 DeleteFlag { get; set; }

    /// <summary>排序</summary>
    public Int32 Sort { get; set; }
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
                "Title" => Title,
                "Width" => Width,
                "Height" => Height,
                "BackgroundColor" => BackgroundColor,
                "BackgroundImage" => BackgroundImage,
                "PresetLine" => PresetLine,
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
                case "Title": Title = Convert.ToString(value); break;
                case "Width": Width = value.ToLong(); break;
                case "Height": Height = value.ToLong(); break;
                case "BackgroundColor": BackgroundColor = Convert.ToString(value); break;
                case "BackgroundImage": BackgroundImage = Convert.ToString(value); break;
                case "PresetLine": PresetLine = Convert.ToString(value); break;
                case "RefreshSeconds": RefreshSeconds = value.ToInt(); break;
                case "EnableFlag": EnableFlag = value.ToInt(); break;
                case "DeleteFlag": DeleteFlag = value.ToInt(); break;
                case "Sort": Sort = value.ToInt(); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ReportDashboardEntity model)
    {
        Id = model.Id;
        ReportCode = model.ReportCode;
        Title = model.Title;
        Width = model.Width;
        Height = model.Height;
        BackgroundColor = model.BackgroundColor;
        BackgroundImage = model.BackgroundImage;
        PresetLine = model.PresetLine;
        RefreshSeconds = model.RefreshSeconds;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
        Sort = model.Sort;
    }
    #endregion
}
