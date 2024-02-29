using CleanArchitecture.Application.Dtos.Auth.Users;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Services.Auth.User;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<List<UserDto>> GetAllUsersAsync(UserPagingQueryInput input);
    Task<PagedResult<UserDto>> GetAllUsersPagedAsync(UserPagingQueryInput input);
    Task CreateUserAsync(CreateUserDto input);
    Task UpdateUserAsync(UpdateUserDto input);
    Task DeleteUsersAsync(int[] ids);
    Task ChangeMyPassWordAsync(ChangeMyPasswordRequest input, int currentUserId);
    Task SetPasswordAsync(SetPasswordRequest input);
    Task ChangeEmailAsync(ChangeEmailRequest input);
    Task AssignRolesToUserAsync(AssignRolesToUserRequest input);
    Task<IList<string>> GetUserRolesAsync(int userId);
}