using Contracts.Common.Models.Files;
using Infrastructure.Cache.Storage;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Common;

public class FileController: ApiControllerBase
{
    private readonly ITempFileCacheManager _tempFileCacheManager;

    public FileController(ITempFileCacheManager tempFileCacheManager)
    {
        _tempFileCacheManager = tempFileCacheManager;
    }

    [HttpPost("download-temp-file")]
    public async Task<ActionResult> DownloadTempFile(FileDto file)
    {
        var fileBytes = await _tempFileCacheManager.GetFile(file.FileToken);
        if (fileBytes == null)
        {
            return NotFound("Requested File Does Not Exists");
        }

        return File(fileBytes, file.FileType, file.FileName);
    }

}