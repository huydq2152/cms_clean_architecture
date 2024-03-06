using System.Net.Http.Headers;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = Contracts.Exceptions.ApplicationException;

namespace CleanArchitecture.WebAPI.Controllers.Common;

public class MediaController : ApiControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UploadImageSettings _uploadImageSettings;

    public MediaController(IWebHostEnvironment webHostEnvironment, UploadImageSettings uploadImageSettings)
    {
        _webHostEnvironment = webHostEnvironment;
        _uploadImageSettings = uploadImageSettings;
    }
    
    [HttpPost]
    public IActionResult UploadImage(string type)
    {
        var allowImageTypes = _uploadImageSettings.AllowImageFileTypes?.Split(",");

        var now = DateTime.Now;
        var files = Request.Form.Files;
        if (files.Count == 0)
        {
            return null;
        }

        var file = files[0];
        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition)?.FileName?.Trim('"');
        if (allowImageTypes?.Any(x => filename?.EndsWith(x, StringComparison.OrdinalIgnoreCase) == true) == false)
        {
            throw new ApplicationException("Không cho phép tải lên file không phải ảnh.");
        }

        var imageFolder = $@"\{_uploadImageSettings.ImageFolder}\{type}\{now:MMyyyy}";

        var folder = _webHostEnvironment.WebRootPath + imageFolder;

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        var filePath = Path.Combine(folder, filename);
        using (var fs = global::System.IO.File.Create(filePath))
        {
            file.CopyTo(fs);
            fs.Flush();
        }
        var path = Path.Combine(imageFolder, filename).Replace(@"\", @"/");
        return Ok(new { path });
    }
}