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

/// <summary>报表。报表主表</summary>
public partial class ReportEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>报表名称</summary>
    public String? ReportName { get; set; }

    /// <summary>报表编码</summary>
    public String? ReportCode { get; set; }

    /// <summary>报表分组</summary>
    public String? ReportGroup { get; set; }

    /// <summary>报表类型。report_screen/report_excel</summary>
    public String? ReportType { get; set; }

    /// <summary>报表缩略图</summary>
    public String? ReportImage { get; set; }

    /// <summary>报表描述</summary>
    public String? ReportDesc { get; set; }

    /// <summary>报表作者</summary>
    public String? ReportAuthor { get; set; }

    /// <summary>下载次数</summary>
    public Int64 DownloadCount { get; set; }

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
                "ReportName" => ReportName,
                "ReportCode" => ReportCode,
                "ReportGroup" => ReportGroup,
                "ReportType" => ReportType,
                "ReportImage" => ReportImage,
                "ReportDesc" => ReportDesc,
                "ReportAuthor" => ReportAuthor,
                "DownloadCount" => DownloadCount,
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
                case "ReportName": ReportName = Convert.ToString(value); break;
                case "ReportCode": ReportCode = Convert.ToString(value); break;
                case "ReportGroup": ReportGroup = Convert.ToString(value); break;
                case "ReportType": ReportType = Convert.ToString(value); break;
                case "ReportImage": ReportImage = Convert.ToString(value); break;
                case "ReportDesc": ReportDesc = Convert.ToString(value); break;
                case "ReportAuthor": ReportAuthor = Convert.ToString(value); break;
                case "DownloadCount": DownloadCount = value.ToLong(); break;
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
    public void Copy(ReportEntity model)
    {
        Id = model.Id;
        ReportName = model.ReportName;
        ReportCode = model.ReportCode;
        ReportGroup = model.ReportGroup;
        ReportType = model.ReportType;
        ReportImage = model.ReportImage;
        ReportDesc = model.ReportDesc;
        ReportAuthor = model.ReportAuthor;
        DownloadCount = model.DownloadCount;
        EnableFlag = model.EnableFlag;
        DeleteFlag = model.DeleteFlag;
    }
    #endregion
}
