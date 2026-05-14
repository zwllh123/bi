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

/// <summary>文件。文件管理</summary>
[Serializable]
[DataObject]
[Description("文件。文件管理")]
[BindIndex("IX_GaeaFile_FileId", false, "FileId")]
[BindTable("GaeaFile", Description = "文件。文件管理", ConnName = "OliveBI", DbType = DatabaseType.None)]
public partial class GaeaFile : IEntity<GaeaFileEntity>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String? _FileId;
    /// <summary>文件唯一UUID</summary>
    [DisplayName("文件唯一UUID")]
    [Description("文件唯一UUID")]
    [DataObjectField(false, false, true, 64)]
    [BindColumn("FileId", "文件唯一UUID", "", Master = true)]
    public String? FileId { get => _FileId; set { if (OnPropertyChanging("FileId", value)) { _FileId = value; OnPropertyChanged("FileId"); } } }

    private String? _FileType;
    /// <summary>文件类型</summary>
    [DisplayName("文件类型")]
    [Description("文件类型")]
    [DataObjectField(false, false, true, 20)]
    [BindColumn("FileType", "文件类型", "")]
    public String? FileType { get => _FileType; set { if (OnPropertyChanging("FileType", value)) { _FileType = value; OnPropertyChanged("FileType"); } } }

    private String? _FilePath;
    /// <summary>文件存储路径</summary>
    [DisplayName("文件存储路径")]
    [Description("文件存储路径")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("FilePath", "文件存储路径", "")]
    public String? FilePath { get => _FilePath; set { if (OnPropertyChanging("FilePath", value)) { _FilePath = value; OnPropertyChanged("FilePath"); } } }

    private String? _UrlPath;
    /// <summary>文件下载Http路径</summary>
    [DisplayName("文件下载Http路径")]
    [Description("文件下载Http路径")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("UrlPath", "文件下载Http路径", "")]
    public String? UrlPath { get => _UrlPath; set { if (OnPropertyChanging("UrlPath", value)) { _UrlPath = value; OnPropertyChanged("UrlPath"); } } }

    private String? _FileInstruction;
    /// <summary>文件内容说明</summary>
    [DisplayName("文件内容说明")]
    [Description("文件内容说明")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("FileInstruction", "文件内容说明", "")]
    public String? FileInstruction { get => _FileInstruction; set { if (OnPropertyChanging("FileInstruction", value)) { _FileInstruction = value; OnPropertyChanged("FileInstruction"); } } }

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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object? this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "FileId" => _FileId,
            "FileType" => _FileType,
            "FilePath" => _FilePath,
            "UrlPath" => _UrlPath,
            "FileInstruction" => _FileInstruction,
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
                case "FileId": _FileId = Convert.ToString(value); break;
                case "FileType": _FileType = Convert.ToString(value); break;
                case "FilePath": _FilePath = Convert.ToString(value); break;
                case "UrlPath": _UrlPath = Convert.ToString(value); break;
                case "FileInstruction": _FileInstruction = Convert.ToString(value); break;
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
    public static GaeaFile? FindById(Int32 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据文件唯一UUID查找</summary>
    /// <param name="fileId">文件唯一UUID</param>
    /// <returns>实体列表</returns>
    public static IList<GaeaFile> FindAllByFileId(String? fileId)
    {
        if (fileId == null) return [];

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.FileId.EqualIgnoreCase(fileId));

        return FindAll(_.FileId == fileId);
    }
    #endregion

    #region 字段名
    /// <summary>取得文件字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>文件唯一UUID</summary>
        public static readonly Field FileId = FindByName("FileId");

        /// <summary>文件类型</summary>
        public static readonly Field FileType = FindByName("FileType");

        /// <summary>文件存储路径</summary>
        public static readonly Field FilePath = FindByName("FilePath");

        /// <summary>文件下载Http路径</summary>
        public static readonly Field UrlPath = FindByName("UrlPath");

        /// <summary>文件内容说明</summary>
        public static readonly Field FileInstruction = FindByName("FileInstruction");

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

    /// <summary>取得文件字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>文件唯一UUID</summary>
        public const String FileId = "FileId";

        /// <summary>文件类型</summary>
        public const String FileType = "FileType";

        /// <summary>文件存储路径</summary>
        public const String FilePath = "FilePath";

        /// <summary>文件下载Http路径</summary>
        public const String UrlPath = "UrlPath";

        /// <summary>文件内容说明</summary>
        public const String FileInstruction = "FileInstruction";

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
