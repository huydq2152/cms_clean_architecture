using CleanArchitecture.Application.Dtos.Auth.Users;
using CleanArchitecture.Application.Extensions.Auth;
using CleanArchitecture.Application.Interfaces.Services.Auth.User;
using CleanArchitecture.WebAPI.Controllers.Common;
using CleanArchitecture.WebAPI.Filter;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.SeedWork.Auth;

namespace CleanArchitecture.WebAPI.Controllers.Auth;

public class UserController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    [Authorize(StaticPermissions.Users.View)]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("all")]
    [Authorize(StaticPermissions.Users.View)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var result = await _userService.GetAllUsersAsync();
        return Ok(result);
    }

    [HttpGet]
    [Route("paging")]
    [Authorize(StaticPermissions.Users.View)]
    public async Task<ActionResult<PagedResult<UserDto>>> GetUsersAllPaging([FromQuery] UserPagingQueryInput input)
    {
        var result = await _userService.GetAllUsersPagedAsync(input);
        return Ok(result);
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(StaticPermissions.Users.Create)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto input)
    {
        await _userService.CreateUserAsync(input);
        return Ok();
    }

    [HttpPut]
    [ValidateModel]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto input)
    {
        await _userService.UpdateUserAsync(input);
        return Ok();
    }

    [HttpDelete]
    [Authorize(StaticPermissions.Users.Delete)]
    public async Task<IActionResult> DeleteUser([FromBody] int[] ids)
    {
        await _userService.DeleteUserAsync(ids);
        return Ok();
    }

    [HttpPost]
    [Route("change-password-current-user")] 
    [ValidateModel]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> ChangeMyPassWord([FromBody] ChangeMyPasswordRequest input)
    {
        await _userService.ChangeMyPassWord(input, User.GetUserId());
        return Ok();
    }

    [HttpPost]
    [Route("set-password")]
    [ValidateModel]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> SetPassword([FromBody] SetPasswordRequest input)
    {
        await _userService.SetPassword(input);
        return Ok();
    }

    [HttpPost]
    [Route("change-email")]
    [ValidateModel]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest input)
    {
        await _userService.ChangeEmail(input);
        return Ok();
    }

    [HttpPost]
    [Route("assign-roles")]
    [ValidateModel]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> AssignRolesToUser([FromBody] AssignRolesToUserRequest input)
    {
        await _userService.AssignRolesToUser(input);
        return Ok();
    }
}