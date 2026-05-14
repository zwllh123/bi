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

/// <summary>数据集转换。数据集结果转换规则</summary>
[Serializable]
[DataObject]
[Description("数据集转换。数据集结果转换规则")]
[BindIndex("IX_ReportDataSetTransform_SetCode", false, "SetCode")]
[BindTable("ReportDataSetTransform", Description = "数据集转换。数据集结果转换规则", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class ReportDataSetTransform : IEntity<ReportDataSetTransformEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String? _SetCode;
    /// <summary>数据集编码</summary>
    [DisplayName("数据集编码")]
    [Description("数据集编码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SetCode", "数据集编码", "")]
    public String? SetCode { get => _SetCode; set { if (OnPropertyChanging("SetCode", value)) { _SetCode = value; OnPropertyChanged("SetCode"); } } }

    private String? _TransformType;
    /// <summary>转换类型。js/javaBean/dict</summary>
    [DisplayName("转换类型")]
    [Description("转换类型。js/javaBean/dict")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("TransformType", "转换类型。js/javaBean/dict", "", Master = true)]
    public String? TransformType { get => _TransformType; set { if (OnPropertyChanging("TransformType", value)) { _TransformType = value; OnPropertyChanged("TransformType"); } } }

    private String? _TransformScript;
    /// <summary>转换脚本/处理逻辑</summary>
    [DisplayName("转换脚本_处理逻辑")]
    [Description("转换脚本/处理逻辑")]
    [DataObjectField(false, false, true, -1)]
    [BindColumn("TransformScript", "转换脚本/处理逻辑", "")]
    public String? TransformScript { get => _TransformScript; set { if (OnPropertyChanging("TransformScript", value)) { _TransformScript = value; OnPropertyChanged("TransformScript"); } } }

    private Int32 _OrderNum;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("OrderNum", "排序", "")]
    public Int32 OrderNum { get => _OrderNum; set { if (OnPropertyChanging("OrderNum", value)) { _OrderNum = value; OnPropertyChanged("OrderNum"); } } }

    private Int32 _EnableFlag;
    /// <summary>启用标记</summary>
    [DisplayName("启用标记")]
    [Description("启用标记")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("EnableFlag", "启用标记", "", DefaultValue = "1")]
    public Int32 EnableFlag { get => _EnableFlag; set { if (OnPropertyChanging("EnableFlag", value)) { _EnableFlag = value; OnPropertyChanged("EnableFlag"); } } }

    private Int32 _DeleteFlag;
    /// <summary>删除标记</summary>
    [DisplayName("删除标记")]
    [Description("删除标记")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("DeleteFlag", "删除标记", "", DefaultValue = "0")]
    public Int32 DeleteFlag { get => _DeleteFlag; set { if (OnPropertyChanging("DeleteFlag", value)) { _DeleteFlag = value; OnPropertyChanged("DeleteFlag"); } } }

    private String? _CreateUser;
    /// <summary>创建者</summary>
    [Category("扩展")]
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 255)]
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
    [DataObjectField(false, false, true, 255)]
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object? this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "SetCode" => _SetCode,
            "TransformType" => _TransformType,
            "TransformScript" => _TransformScript,
            "OrderNum" => _OrderNum,
            "EnableFlag" => _EnableFlag,
            "DeleteFlag" => _DeleteFlag,
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
                case "SetCode": _SetCode = Convert.ToString(value); break;
                case "TransformType": _TransformType = Convert.ToString(value); break;
                case "TransformScript": _TransformScript = Convert.ToString(value); break;
                case "OrderNum": _OrderNum = value.ToInt(); break;
                case "EnableFlag": _EnableFlag = value.ToInt(); break;
                case "DeleteFlag": _DeleteFlag = value.ToInt(); break;
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
    public static ReportDataSetTransform? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据数据集编码查找</summary>
    /// <param name="setCode">数据集编码</param>
    /// <returns>实体列表</returns>
    public static IList<ReportDataSetTransform> FindAllBySetCode(String? setCode)
    {
        if (setCode == null) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.SetCode.EqualIgnoreCase(setCode));

        return FindAll(_.SetCode == setCode);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="setCode">数据集编码</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<ReportDataSetTransform> Search(String? setCode, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!setCode.IsNullOrEmpty()) exp &= _.SetCode == setCode;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得数据集转换字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>数据集编码</summary>
        public static readonly Field SetCode = FindByName("SetCode");

        /// <summary>转换类型。js/javaBean/dict</summary>
        public static readonly Field TransformType = FindByName("TransformType");

        /// <summary>转换脚本/处理逻辑</summary>
        public static readonly Field TransformScript = FindByName("TransformScript");

        /// <summary>排序</summary>
        public static readonly Field OrderNum = FindByName("OrderNum");

        /// <summary>启用标记</summary>
        public static readonly Field EnableFlag = FindByName("EnableFlag");

        /// <summary>删除标记</summary>
        public static readonly Field DeleteFlag = FindByName("DeleteFlag");

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

    /// <summary>取得数据集转换字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>数据集编码</summary>
        public const String SetCode = "SetCode";

        /// <summary>转换类型。js/javaBean/dict</summary>
        public const String TransformType = "TransformType";

        /// <summary>转换脚本/处理逻辑</summary>
        public const String TransformScript = "TransformScript";

        /// <summary>排序</summary>
        public const String OrderNum = "OrderNum";

        /// <summary>启用标记</summary>
        public const String EnableFlag = "EnableFlag";

        /// <summary>删除标记</summary>
        public const String DeleteFlag = "DeleteFlag";

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
