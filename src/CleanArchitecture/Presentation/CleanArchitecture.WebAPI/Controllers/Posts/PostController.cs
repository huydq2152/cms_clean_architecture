using CleanArchitecture.Application.Dtos.Posts.Post;
using CleanArchitecture.Application.Excels.Exporting.Dtos;
using CleanArchitecture.Application.Excels.Importing.Dtos;
using CleanArchitecture.Application.Excels.Interfaces;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.Domain.Consts;
using CleanArchitecture.Domain.Entities.Apps;
using CleanArchitecture.Domain.StaticData.Auth;
using CleanArchitecture.Persistence.Storage;
using CleanArchitecture.WebAPI.Controllers.Common;
using CleanArchitecture.WebAPI.Filter;
using Contracts.Common.Models.Files;
using Contracts.Common.Models.Paging;
using Contracts.ScheduledJobs;
using Infrastructure.Extensions.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Posts;

public class PostController : ApiControllerBase
{
    private readonly IPostService _postService;
    private readonly IPostListExcelExporter _postListExcelExporter;
    private readonly IBinaryObjectManager _binaryObjectManager;
    private readonly IScheduledJobService _scheduledJobService;

    public PostController(IPostService postService, IPostListExcelExporter postListExcelExporter,
        IBinaryObjectManager binaryObjectManager, IScheduledJobService scheduledJobService)
    {
        _postService = postService;
        _postListExcelExporter = postListExcelExporter;
        _binaryObjectManager = binaryObjectManager;
        _scheduledJobService = scheduledJobService;
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [Authorize(StaticPermissions.Posts.View)]
    public async Task<ActionResult<PostDto>> GetPostByIdAsync(int id)
    {
        var result = await _postService.GetPostByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("all")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [Authorize(StaticPermissions.Posts.View)]
    public async Task<ActionResult<List<PostDto>>> GetAllPostsAsync(
        [FromBody] GetAllPostsInput input)
    {
        var result = await _postService.GetAllPostsAsync(input);
        return Ok(result);
    }

    [HttpPost("paging")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [Authorize(StaticPermissions.Posts.View)]
    public async Task<ActionResult<PagedResult<PostDto>>> GetAllPostPagedAsync(
        [FromBody] GetAllPostsInput input)
    {
        var result = await _postService.GetAllPostPagedAsync(input);
        return Ok(result);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationModelAttribute<CreatePostDto>))]
    [Authorize(StaticPermissions.Posts.Create)]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostDto input)
    {
        await _postService.CreatePostAsync(input);
        return Ok();
    }

    [HttpPut]
    [ServiceFilter(typeof(ValidationModelAttribute<UpdatePostDto>))]
    [Authorize(StaticPermissions.Posts.Edit)]
    public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostDto input)
    {
        await _postService.UpdatePostAsync(input);
        return Ok();
    }

    [HttpDelete]
    [Authorize(StaticPermissions.Posts.Delete)]
    public async Task<IActionResult> DeletePostAsync([FromBody] int[] ids)
    {
        await _postService.DeletePostsAsync(ids);
        return Ok();
    }

    [HttpPost("export-posts")]
    public async Task<ActionResult<FileDto>> GetExportPosts(GetExportPostsInput input)
    {
        var posts = await _postService.GetAllPostsForExportAsync(input);
        if (posts.Count == 0)
        {
            return new FileDto();
        }

        var file = _postListExcelExporter.ExportToFile(posts);

        return Ok(file);
    }

    [HttpPost]
    [Authorize(StaticPermissions.Posts.Create)]
    public async Task<ActionResult<JsonResult>> ImportFromExcel()
    {
        var file = Request.Form.Files.First();

        if (file == null)
        {
            return BadRequest("Vui lòng chọn file để import");
        }

        if (file.Length > AppConsts.ImportFromExcelMaxFileLength)
        {
            return BadRequest("Dung lượng file đã vượt quá giới hạn cho phép. Vui lòng chọn file khác.");
        }

        byte[] fileBytes;
        await using (var stream = file.OpenReadStream())
        {
            fileBytes = stream.GetAllBytes();
        }

        var fileObject = new BinaryObject(fileBytes, $"{DateTime.UtcNow} import from excel file.");
 
        await _binaryObjectManager.CreateBinaryObjectAsync(fileObject);

        var importPostsFromExcelJobInput = new ImportPostFromExcelJobInput
        {
            BinaryObjectId = fileObject.Id,
            UserId = 1
        };
        
        var jobId = _scheduledJobService.Enqueue(() => _postService.ImportPostsFromExcelAsync(importPostsFromExcelJobInput));
        
        return Ok(jobId);
    }
}