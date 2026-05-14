using Microsoft.AspNetCore.Mvc;
using NewLife;
using Olive.BI.Entity;
using Rock.Common;
using Rock.Common.Model;

namespace Olive.BI.Web.Controllers;

/// <summary>文件上传/下载</summary>
[ApiController]
[Route("file")]
public class FileController : EntityController
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;

    /// <summary>构造</summary>
    /// <param name="config">配置</param>
    /// <param name="env">主机环境</param>
    public FileController(IConfiguration config, IWebHostEnvironment env)
    {
        _config = config;
        _env = env;
    }

    /// <summary>上传响应</summary>
    public class UploadResponse
    {
        /// <summary>文件唯一ID</summary>
        public String FileId { get; set; } = null!;

        /// <summary>下载URL</summary>
        public String? UrlPath { get; set; }

        /// <summary>文件类型</summary>
        public String? FileType { get; set; }
    }

    /// <summary>上传文件</summary>
    /// <param name="file">文件</param>
    /// <param name="instruction">说明</param>
    /// <returns>上传结果</returns>
    [HttpPost("upload")]
    public async Task<Result<UploadResponse>> Upload(IFormFile file, [FromForm] String? instruction)
    {
        if (file == null || file.Length <= 0)
            return new Result<UploadResponse> { State = 1, Msg = "未选择文件" };

        var rootPath = _config["Upload:RootPath"] ?? "wwwroot/uploads";
        var urlPrefix = _config["Upload:UrlPrefix"] ?? "/uploads";

        // 解析为绝对路径
        var baseDir = Path.IsPathRooted(rootPath) ? rootPath : Path.Combine(_env.ContentRootPath, rootPath);

        var fileId = Guid.NewGuid().ToString("N");
        var ext = Path.GetExtension(file.FileName) ?? "";
        var fileType = ext.TrimStart('.').ToLowerInvariant();

        // 按日期分目录
        var subDir = DateTime.Now.ToString("yyyyMM");
        var targetDir = Path.Combine(baseDir, subDir);
        if (!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);

        var fileName = fileId + ext;
        var fullPath = Path.Combine(targetDir, fileName);

        using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fs);
        }

        var urlPath = $"{urlPrefix.TrimEnd('/')}/{subDir}/{fileName}";

        var entity = new GaeaFile
        {
            FileId = fileId,
            FileType = fileType,
            FilePath = fullPath,
            UrlPath = urlPath,
            FileInstruction = instruction,
        };
        entity.Insert();

        return ResultHelper.ToResult(new UploadResponse
        {
            FileId = fileId,
            UrlPath = urlPath,
            FileType = fileType,
        });
    }

    /// <summary>按 FileId 下载</summary>
    /// <param name="fileId">文件ID</param>
    /// <returns>文件流</returns>
    [HttpGet("download/{fileId}")]
    public IActionResult Download(String fileId)
    {
        if (fileId.IsNullOrEmpty()) return NotFound();

        var entity = GaeaFile.FindByFileId(fileId);
        if (entity == null || entity.FilePath.IsNullOrEmpty() || !System.IO.File.Exists(entity.FilePath))
            return NotFound();

        var fileName = $"{entity.FileId}.{entity.FileType}";
        return PhysicalFile(entity.FilePath!, "application/octet-stream", fileName);
    }

    /// <summary>按 FileId 删除</summary>
    /// <param name="req">Id请求（Id=主键Id；此处不用 FileId 是因为 IdRequest 是 Int32）</param>
    /// <returns>结果</returns>
    [HttpPost("delete")]
    public Result Delete([FromBody] IdRequest req)
    {
        if (req == null || req.Id <= 0) return new Result { State = 1, Msg = "Id必填" };

        var entity = GaeaFile.FindById(req.Id);
        if (entity == null) return new Result { State = 1, Msg = "文件不存在" };

        if (!entity.FilePath.IsNullOrEmpty() && System.IO.File.Exists(entity.FilePath))
        {
            try { System.IO.File.Delete(entity.FilePath!); } catch { }
        }

        entity.Delete();
        return new Result().ToResult();
    }

    /// <summary>查询文件信息</summary>
    /// <param name="fileId">文件ID</param>
    /// <returns>文件信息</returns>
    [HttpGet("info/{fileId}")]
    public Result<GaeaFileEntity> Info(String fileId)
    {
        if (fileId.IsNullOrEmpty()) return new Result<GaeaFileEntity> { State = 1, Msg = "FileId必填" };

        var entity = GaeaFile.FindByFileId(fileId);
        if (entity == null) return new Result<GaeaFileEntity> { State = 1, Msg = "文件不存在" };

        return ResultHelper.ToResult(entity.ToModel());
    }
}
