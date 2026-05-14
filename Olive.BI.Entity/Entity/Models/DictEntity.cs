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

/// <summary>数据字典</summary>
public partial class DictEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>字典名称</summary>
    public String DictName { get; set; } = null!;

    /// <summary>字典编码</summary>
    public String DictCode { get; set; } = null!;

    /// <summary>备注</summary>
    public String? Remark { get; set; }
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
                "DictName" => DictName,
                "DictCode" => DictCode,
                "Remark" => Remark,
                _ => this.GetValue(name, false),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "DictName": DictName = Convert.ToString(value); break;
                case "DictCode": DictCode = Convert.ToString(value); break;
                case "Remark": Remark = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(DictEntity model)
    {
        Id = model.Id;
        DictName = model.DictName;
        DictCode = model.DictCode;
        Remark = model.Remark;
    }
    #endregion
}
