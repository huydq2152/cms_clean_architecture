using CleanArchitecture.Application.Dtos.Auth.Users;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Interfaces.Services.Auth.User;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<PagedResult<UserDto>> GetAllUsersPagedAsync(UserPagingQueryInput input);
    Task CreateUserAsync(CreateUserDto input);
    Task UpdateUserAsync(UpdateUserDto input);
    Task DeleteUserAsync(int[] ids);
    Task ChangeMyPassWord(ChangeMyPasswordRequest input, int currentUserId);
    Task SetPassword(SetPasswordRequest input);
    Task ChangeEmail(ChangeEmailRequest input);
    Task AssignRolesToUser(AssignRolesToUserRequest input);
}