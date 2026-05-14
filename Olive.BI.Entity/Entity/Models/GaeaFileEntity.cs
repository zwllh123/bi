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

/// <summary>文件。文件管理</summary>
public partial class GaeaFileEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>文件唯一UUID</summary>
    public String? FileId { get; set; }

    /// <summary>文件类型</summary>
    public String? FileType { get; set; }

    /// <summary>文件存储路径</summary>
    public String? FilePath { get; set; }

    /// <summary>文件下载Http路径</summary>
    public String? UrlPath { get; set; }

    /// <summary>文件内容说明</summary>
    public String? FileInstruction { get; set; }
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
                "FileId" => FileId,
                "FileType" => FileType,
                "FilePath" => FilePath,
                "UrlPath" => UrlPath,
                "FileInstruction" => FileInstruction,
                _ => this.GetValue(name, false),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "FileId": FileId = Convert.ToString(value); break;
                case "FileType": FileType = Convert.ToString(value); break;
                case "FilePath": FilePath = Convert.ToString(value); break;
                case "UrlPath": UrlPath = Convert.ToString(value); break;
                case "FileInstruction": FileInstruction = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(GaeaFileEntity model)
    {
        Id = model.Id;
        FileId = model.FileId;
        FileType = model.FileType;
        FilePath = model.FilePath;
        UrlPath = model.UrlPath;
        FileInstruction = model.FileInstruction;
    }
    #endregion
}
