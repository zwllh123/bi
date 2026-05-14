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

/// <summary>字典项</summary>
public partial class DictItemEntity : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>字典编码</summary>
    public String? DictCode { get; set; }

    /// <summary>字典项名称</summary>
    public String ItemName { get; set; } = null!;

    /// <summary>字典项值</summary>
    public String ItemValue { get; set; } = null!;

    /// <summary>字典扩展项</summary>
    public String? ItemExtend { get; set; }

    /// <summary>启用。1-启用 0-禁用</summary>
    public Int32 Enabled { get; set; }

    /// <summary>语言标识</summary>
    public String? Locale { get; set; }

    /// <summary>备注</summary>
    public String? Remark { get; set; }

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
                "DictCode" => DictCode,
                "ItemName" => ItemName,
                "ItemValue" => ItemValue,
                "ItemExtend" => ItemExtend,
                "Enabled" => Enabled,
                "Locale" => Locale,
                "Remark" => Remark,
                "Sort" => Sort,
                _ => this.GetValue(name, false),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "DictCode": DictCode = Convert.ToString(value); break;
                case "ItemName": ItemName = Convert.ToString(value); break;
                case "ItemValue": ItemValue = Convert.ToString(value); break;
                case "ItemExtend": ItemExtend = Convert.ToString(value); break;
                case "Enabled": Enabled = value.ToInt(); break;
                case "Locale": Locale = Convert.ToString(value); break;
                case "Remark": Remark = Convert.ToString(value); break;
                case "Sort": Sort = value.ToInt(); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(DictItemEntity model)
    {
        Id = model.Id;
        DictCode = model.DictCode;
        ItemName = model.ItemName;
        ItemValue = model.ItemValue;
        ItemExtend = model.ItemExtend;
        Enabled = model.Enabled;
        Locale = model.Locale;
        Remark = model.Remark;
        Sort = model.Sort;
    }
    #endregion
}
