using NewLife;

namespace Olive.BI.Entity;

/// <summary>文件。业务扩展</summary>
public partial class GaeaFile
{
    #region 业务方法

    /// <summary>按 FileId 查找首条记录</summary>
    /// <param name="fileId">文件唯一ID</param>
    /// <returns>文件实体，未找到返回null</returns>
    public static GaeaFile? FindByFileId(String? fileId)
    {
        if (fileId.IsNullOrEmpty()) return null;

        return FindAllByFileId(fileId).FirstOrDefault();
    }

    #endregion
}
