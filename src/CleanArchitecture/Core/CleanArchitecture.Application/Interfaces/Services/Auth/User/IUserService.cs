using CleanArchitecture.Application.Dtos.Auth.Users;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Interfaces.Services.Auth.User;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<PagedResult<UserDto>> GetAllUsersPagedAsync(UserPagingQueryInput input);
    Task<IdentityResult> CreateUserAsync(CreateUserDto input);
    Task<IdentityResult> UpdateUserAsync(UpdateUserDto input);
    Task DeleteUserAsync(int[] ids);
    Task<IdentityResult> ChangeMyPassWord(ChangeMyPasswordRequest input);
    Task<IdentityResult> SetPassword(SetPasswordRequest input);
    Task<IdentityResult> ChangeEmail(ChangeEmailRequest input);
    Task<IdentityResult> AssignRolesToUser(AssignRolesToUserRequest input);
}