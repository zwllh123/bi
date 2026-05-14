using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Olive.BI.Entity;

/// <summary>字典项</summary>
[Serializable]
[DataObject]
[Description("字典项")]
[BindIndex("IX_DictItem_DictCode", false, "DictCode")]
[BindIndex("IX_DictItem_DictCode_ItemValue", false, "DictCode,ItemValue")]
[BindTable("DictItem", Description = "字典项", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class DictItem : IEntity<DictItemEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String? _DictCode;
    /// <summary>字典编码</summary>
    [DisplayName("字典编码")]
    [Description("字典编码")]
    [DataObjectField(false, false, true, 64)]
    [BindColumn("DictCode", "字典编码", "")]
    public String? DictCode { get => _DictCode; set { if (OnPropertyChanging("DictCode", value)) { _DictCode = value; OnPropertyChanged("DictCode"); } } }

    private String _ItemName = null!;
    /// <summary>字典项名称</summary>
    [DisplayName("字典项名称")]
    [Description("字典项名称")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("ItemName", "字典项名称", "", Master = true)]
    public String ItemName { get => _ItemName; set { if (OnPropertyChanging("ItemName", value)) { _ItemName = value; OnPropertyChanged("ItemName"); } } }

    private String _ItemValue = null!;
    /// <summary>字典项值</summary>
    [DisplayName("字典项值")]
    [Description("字典项值")]
    [DataObjectField(false, false, false, 64)]
    [BindColumn("ItemValue", "字典项值", "")]
    public String ItemValue { get => _ItemValue; set { if (OnPropertyChanging("ItemValue", value)) { _ItemValue = value; OnPropertyChanged("ItemValue"); } } }

    private String? _ItemExtend;
    /// <summary>字典扩展项</summary>
    [DisplayName("字典扩展项")]
    [Description("字典扩展项")]
    [DataObjectField(false, false, true, 2048)]
    [BindColumn("ItemExtend", "字典扩展项", "")]
    public String? ItemExtend { get => _ItemExtend; set { if (OnPropertyChanging("ItemExtend", value)) { _ItemExtend = value; OnPropertyChanged("ItemExtend"); } } }

    private Int32 _Enabled;
    /// <summary>启用。1-启用 0-禁用</summary>
    [DisplayName("启用")]
    [Description("启用。1-启用 0-禁用")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Enabled", "启用。1-启用 0-禁用", "", DefaultValue = "1")]
    public Int32 Enabled { get => _Enabled; set { if (OnPropertyChanging("Enabled", value)) { _Enabled = value; OnPropertyChanged("Enabled"); } } }

    private String? _Locale;
    /// <summary>语言标识</summary>
    [DisplayName("语言标识")]
    [Description("语言标识")]
    [DataObjectField(false, false, true, 16)]
    [BindColumn("Locale", "语言标识", "")]
    public String? Locale { get => _Locale; set { if (OnPropertyChanging("Locale", value)) { _Locale = value; OnPropertyChanged("Locale"); } } }

    private String? _Remark;
    /// <summary>备注</summary>
    [DisplayName("备注")]
    [Description("备注")]
    [DataObjectField(false, false, true, 64)]
    [BindColumn("Remark", "备注", "")]
    public String? Remark { get => _Remark; set { if (OnPropertyChanging("Remark", value)) { _Remark = value; OnPropertyChanged("Remark"); } } }

    private Int32 _Sort;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sort", "排序", "")]
    public Int32 Sort { get => _Sort; set { if (OnPropertyChanging("Sort", value)) { _Sort = value; OnPropertyChanged("Sort"); } } }

    private String? _CreateUser;
    /// <summary>创建者</summary>
    [Category("扩展")]
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 64)]
    [BindColumn("CreateUser", "创建者", "")]
    public String? CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [Category("扩展")]
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String? _UpdateUser;
    /// <summary>更新者</summary>
    [Category("扩展")]
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, true, 64)]
    [BindColumn("UpdateUser", "更新者", "")]
    public String? UpdateUser { get => _UpdateUser; set { if (OnPropertyChanging("UpdateUser", value)) { _UpdateUser = value; OnPropertyChanged("UpdateUser"); } } }

    private DateTime _UpdateTime;
    /// <summary>更新时间</summary>
    [Category("扩展")]
    [DisplayName("更新时间")]
    [Description("更新时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("UpdateTime", "更新时间", "")]
    public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

    private Int32 _Version;
    /// <summary>版本号</summary>
    [Category("扩展")]
    [DisplayName("版本号")]
    [Description("版本号")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Version", "版本号", "")]
    public Int32 Version { get => _Version; set { if (OnPropertyChanging("Version", value)) { _Version = value; OnPropertyChanged("Version"); } } }
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object? this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "DictCode" => _DictCode,
            "ItemName" => _ItemName,
            "ItemValue" => _ItemValue,
            "ItemExtend" => _ItemExtend,
            "Enabled" => _Enabled,
            "Locale" => _Locale,
            "Remark" => _Remark,
            "Sort" => _Sort,
            "CreateUser" => _CreateUser,
            "CreateTime" => _CreateTime,
            "UpdateUser" => _UpdateUser,
            "UpdateTime" => _UpdateTime,
            "Version" => _Version,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "DictCode": _DictCode = Convert.ToString(value); break;
                case "ItemName": _ItemName = Convert.ToString(value); break;
                case "ItemValue": _ItemValue = Convert.ToString(value); break;
                case "ItemExtend": _ItemExtend = Convert.ToString(value); break;
                case "Enabled": _Enabled = value.ToInt(); break;
                case "Locale": _Locale = Convert.ToString(value); break;
                case "Remark": _Remark = Convert.ToString(value); break;
                case "Sort": _Sort = value.ToInt(); break;
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "UpdateUser": _UpdateUser = Convert.ToString(value); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                case "Version": _Version = value.ToInt(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static DictItem? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据字典编码查找</summary>
    /// <param name="dictCode">字典编码</param>
    /// <returns>实体列表</returns>
    public static IList<DictItem> FindAllByDictCode(String? dictCode)
    {
        if (dictCode == null) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.DictCode.EqualIgnoreCase(dictCode));

        return FindAll(_.DictCode == dictCode);
    }

    /// <summary>根据字典编码、字典项值查找</summary>
    /// <param name="dictCode">字典编码</param>
    /// <param name="itemValue">字典项值</param>
    /// <returns>实体列表</returns>
    public static IList<DictItem> FindAllByDictCodeAndItemValue(String? dictCode, String itemValue)
    {
        if (dictCode == null) return [];
        if (itemValue.IsNullOrEmpty()) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.DictCode.EqualIgnoreCase(dictCode) && e.ItemValue.EqualIgnoreCase(itemValue));

        return FindAll(_.DictCode == dictCode & _.ItemValue == itemValue);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="dictCode">字典编码</param>
    /// <param name="itemValue">字典项值</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<DictItem> Search(String? dictCode, String itemValue, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!dictCode.IsNullOrEmpty()) exp &= _.DictCode == dictCode;
        if (!itemValue.IsNullOrEmpty()) exp &= _.ItemValue == itemValue;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得字典项字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>字典编码</summary>
        public static readonly Field DictCode = FindByName("DictCode");

        /// <summary>字典项名称</summary>
        public static readonly Field ItemName = FindByName("ItemName");

        /// <summary>字典项值</summary>
        public static readonly Field ItemValue = FindByName("ItemValue");

        /// <summary>字典扩展项</summary>
        public static readonly Field ItemExtend = FindByName("ItemExtend");

        /// <summary>启用。1-启用 0-禁用</summary>
        public static readonly Field Enabled = FindByName("Enabled");

        /// <summary>语言标识</summary>
        public static readonly Field Locale = FindByName("Locale");

        /// <summary>备注</summary>
        public static readonly Field Remark = FindByName("Remark");

        /// <summary>排序</summary>
        public static readonly Field Sort = FindByName("Sort");

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUser = FindByName("UpdateUser");

        /// <summary>更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

        /// <summary>版本号</summary>
        public static readonly Field Version = FindByName("Version");

        static Field FindByName(String name) => Meta.Table.FindByName(name)!;
    }

    /// <summary>取得字典项字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>字典编码</summary>
        public const String DictCode = "DictCode";

        /// <summary>字典项名称</summary>
        public const String ItemName = "ItemName";

        /// <summary>字典项值</summary>
        public const String ItemValue = "ItemValue";

        /// <summary>字典扩展项</summary>
        public const String ItemExtend = "ItemExtend";

        /// <summary>启用。1-启用 0-禁用</summary>
        public const String Enabled = "Enabled";

        /// <summary>语言标识</summary>
        public const String Locale = "Locale";

        /// <summary>备注</summary>
        public const String Remark = "Remark";

        /// <summary>排序</summary>
        public const String Sort = "Sort";

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>更新者</summary>
        public const String UpdateUser = "UpdateUser";

        /// <summary>更新时间</summary>
        public const String UpdateTime = "UpdateTime";

        /// <summary>版本号</summary>
        public const String Version = "Version";
    }
    #endregion
}
