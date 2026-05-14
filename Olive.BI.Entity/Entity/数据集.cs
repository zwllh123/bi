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

/// <summary>数据集。数据集管理</summary>
[Serializable]
[DataObject]
[Description("数据集。数据集管理")]
[BindIndex("IU_ReportDataSet_SetCode", true, "SetCode")]
[BindIndex("IX_ReportDataSet_SourceCode", false, "SourceCode")]
[BindTable("ReportDataSet", Description = "数据集。数据集管理", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class ReportDataSet : IEntity<ReportDataSetEntity>
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
    [BindColumn("SetCode", "数据集编码", "", Master = true)]
    public String? SetCode { get => _SetCode; set { if (OnPropertyChanging("SetCode", value)) { _SetCode = value; OnPropertyChanged("SetCode"); } } }

    private String? _SetName;
    /// <summary>数据集名称</summary>
    [DisplayName("数据集名称")]
    [Description("数据集名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("SetName", "数据集名称", "")]
    public String? SetName { get => _SetName; set { if (OnPropertyChanging("SetName", value)) { _SetName = value; OnPropertyChanged("SetName"); } } }

    private String? _SetDesc;
    /// <summary>数据集描述</summary>
    [DisplayName("数据集描述")]
    [Description("数据集描述")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("SetDesc", "数据集描述", "")]
    public String? SetDesc { get => _SetDesc; set { if (OnPropertyChanging("SetDesc", value)) { _SetDesc = value; OnPropertyChanged("SetDesc"); } } }

    private String? _SourceCode;
    /// <summary>数据源编码</summary>
    [DisplayName("数据源编码")]
    [Description("数据源编码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SourceCode", "数据源编码", "")]
    public String? SourceCode { get => _SourceCode; set { if (OnPropertyChanging("SourceCode", value)) { _SourceCode = value; OnPropertyChanged("SourceCode"); } } }

    private String? _DynSentence;
    /// <summary>动态查询SQL或请求体</summary>
    [DisplayName("动态查询SQL或请求体")]
    [Description("动态查询SQL或请求体")]
    [DataObjectField(false, false, true, 2048)]
    [BindColumn("DynSentence", "动态查询SQL或请求体", "")]
    public String? DynSentence { get => _DynSentence; set { if (OnPropertyChanging("DynSentence", value)) { _DynSentence = value; OnPropertyChanged("DynSentence"); } } }

    private String? _CaseResult;
    /// <summary>结果案例</summary>
    [DisplayName("结果案例")]
    [Description("结果案例")]
    [DataObjectField(false, false, true, -1)]
    [BindColumn("CaseResult", "结果案例", "")]
    public String? CaseResult { get => _CaseResult; set { if (OnPropertyChanging("CaseResult", value)) { _CaseResult = value; OnPropertyChanged("CaseResult"); } } }

    private String? _SetType;
    /// <summary>数据集类型。sql/http</summary>
    [DisplayName("数据集类型")]
    [Description("数据集类型。sql/http")]
    [DataObjectField(false, false, true, 10)]
    [BindColumn("SetType", "数据集类型。sql/http", "")]
    public String? SetType { get => _SetType; set { if (OnPropertyChanging("SetType", value)) { _SetType = value; OnPropertyChanged("SetType"); } } }

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
            "SetName" => _SetName,
            "SetDesc" => _SetDesc,
            "SourceCode" => _SourceCode,
            "DynSentence" => _DynSentence,
            "CaseResult" => _CaseResult,
            "SetType" => _SetType,
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
                case "SetName": _SetName = Convert.ToString(value); break;
                case "SetDesc": _SetDesc = Convert.ToString(value); break;
                case "SourceCode": _SourceCode = Convert.ToString(value); break;
                case "DynSentence": _DynSentence = Convert.ToString(value); break;
                case "CaseResult": _CaseResult = Convert.ToString(value); break;
                case "SetType": _SetType = Convert.ToString(value); break;
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
    public static ReportDataSet? FindById(Int32 id)
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
    /// <returns>实体对象</returns>
    public static ReportDataSet? FindBySetCode(String? setCode)
    {
        if (setCode == null) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.SetCode.EqualIgnoreCase(setCode));

        // 单对象缓存
        return Meta.SingleCache.GetItemWithSlaveKey(setCode) as ReportDataSet;

        //return Find(_.SetCode == setCode);
    }

    /// <summary>根据数据源编码查找</summary>
    /// <param name="sourceCode">数据源编码</param>
    /// <returns>实体列表</returns>
    public static IList<ReportDataSet> FindAllBySourceCode(String? sourceCode)
    {
        if (sourceCode == null) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.SourceCode.EqualIgnoreCase(sourceCode));

        return FindAll(_.SourceCode == sourceCode);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="sourceCode">数据源编码</param>
    /// <param name="start">更新时间开始</param>
    /// <param name="end">更新时间结束</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<ReportDataSet> Search(String? sourceCode, DateTime start, DateTime end, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!sourceCode.IsNullOrEmpty()) exp &= _.SourceCode == sourceCode;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

        return FindAll(exp, page);
    }
    #endregion

    #region 字段名
    /// <summary>取得数据集字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>数据集编码</summary>
        public static readonly Field SetCode = FindByName("SetCode");

        /// <summary>数据集名称</summary>
        public static readonly Field SetName = FindByName("SetName");

        /// <summary>数据集描述</summary>
        public static readonly Field SetDesc = FindByName("SetDesc");

        /// <summary>数据源编码</summary>
        public static readonly Field SourceCode = FindByName("SourceCode");

        /// <summary>动态查询SQL或请求体</summary>
        public static readonly Field DynSentence = FindByName("DynSentence");

        /// <summary>结果案例</summary>
        public static readonly Field CaseResult = FindByName("CaseResult");

        /// <summary>数据集类型。sql/http</summary>
        public static readonly Field SetType = FindByName("SetType");

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

    /// <summary>取得数据集字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>数据集编码</summary>
        public const String SetCode = "SetCode";

        /// <summary>数据集名称</summary>
        public const String SetName = "SetName";

        /// <summary>数据集描述</summary>
        public const String SetDesc = "SetDesc";

        /// <summary>数据源编码</summary>
        public const String SourceCode = "SourceCode";

        /// <summary>动态查询SQL或请求体</summary>
        public const String DynSentence = "DynSentence";

        /// <summary>结果案例</summary>
        public const String CaseResult = "CaseResult";

        /// <summary>数据集类型。sql/http</summary>
        public const String SetType = "SetType";

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
