using CleanArchitecture.Application.Dtos.Auth.Users;
using CleanArchitecture.Application.Extensions.Auth;
using CleanArchitecture.Application.Interfaces.Services.Auth.User;
using CleanArchitecture.Domain.StaticData.Auth;
using CleanArchitecture.WebAPI.Controllers.Common;
using CleanArchitecture.WebAPI.Filter;
using Contracts.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<UserDto>> GetUserByIdAsync(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        result.Roles = await _userService.GetUserRolesAsync(id);
        return Ok(result);
    }

    [HttpPost("all")]
    [Authorize(StaticPermissions.Users.View)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync([FromBody] GetAllUsersInput input)
    {
        var result = await _userService.GetAllUsersAsync(input);
        return Ok(result);
    }

    [HttpPost("paging")]
    [Authorize(StaticPermissions.Users.View)]
    public async Task<ActionResult<PagedResult<UserDto>>> GetAllUsersPagedAsync([FromBody] GetAllUsersInput input)
    {
        var result = await _userService.GetAllUsersPagedAsync(input);
        return Ok(result);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationModelAttribute<CreateUserDto>))]
    [Authorize(StaticPermissions.Users.Create)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto input)
    {
        await _userService.CreateUserAsync(input);
        return Ok();
    }

    [HttpPut]
    [ServiceFilter(typeof(ValidationModelAttribute<UpdateUserDto>))]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto input)
    {
        await _userService.UpdateUserAsync(input);
        return Ok();
    }

    [HttpDelete]
    [Authorize(StaticPermissions.Users.Delete)]
    public async Task<IActionResult> DeleteUserAsync([FromBody] int[] ids)
    {
        await _userService.DeleteUsersAsync(ids);
        return Ok();
    }

    [HttpPost]
    [Route("change-password-current-user")]
    [ServiceFilter(typeof(ValidationModelAttribute<ChangeMyPasswordRequest>))]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> ChangeMyPassWordAsync([FromBody] ChangeMyPasswordRequest input)
    {
        await _userService.ChangeMyPassWordAsync(input, User.GetUserId());
        return Ok();
    }

    [HttpPost]
    [Route("set-password")]
    [ServiceFilter(typeof(ValidationModelAttribute<SetPasswordRequest>))]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> SetPasswordAsync([FromBody] SetPasswordRequest input)
    {
        await _userService.SetPasswordAsync(input);
        return Ok();
    }

    [HttpPost]
    [Route("change-email")]
    [ServiceFilter(typeof(ValidationModelAttribute<ChangeEmailRequest>))]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmailRequest input)
    {
        await _userService.ChangeEmailAsync(input);
        return Ok();
    }

    [HttpPost]
    [Route("assign-roles")]
    [ServiceFilter(typeof(ValidationModelAttribute<AssignRolesToUserRequest>))]
    [Authorize(StaticPermissions.Users.Edit)]
    public async Task<IActionResult> AssignRolesToUserAsync([FromBody] AssignRolesToUserRequest input)
    {
        await _userService.AssignRolesToUserAsync(input);
        return Ok();
    }
}