using CleanArchitecture.Application.Dtos.Auth.Users;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Application.Interfaces.Repositories.Auth;

public interface IUserRepository
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<List<UserDto>> GetAllUsersAsync(GetAllUsersInput input);
    Task<PagedResult<UserDto>> GetAllUsersPagedAsync(GetAllUsersInput input);
    Task CreateUserAsync(CreateUserDto input);
    Task UpdateUserAsync(UpdateUserDto input);
    Task DeleteUsersAsync(int[] ids);
    Task ChangeMyPassWordAsync(ChangeMyPasswordRequest input, int currentUserId);
    Task SetPasswordAsync(SetPasswordRequest input);
    Task ChangeEmailAsync(ChangeEmailRequest input);
    Task AssignRolesToUserAsync(AssignRolesToUserRequest input);
    Task<IList<string>> GetUserRolesAsync(int userId);
}