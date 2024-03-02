using CleanArchitecture.Application.Dtos.Auth.Users;
using CleanArchitecture.Application.Interfaces.Repositories.Auth;
using CleanArchitecture.Application.Interfaces.Services.Auth.User;
using Infrastructure.Common.Models.Paging;

namespace CleanArchitecture.Infrastructure.Services.Auth.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var result = await _userRepository.GetUserByIdAsync(id);
        return result;
    }

    public async Task<List<UserDto>> GetAllUsersAsync(GetAllUsersInput input)
    {
        var result = await _userRepository.GetAllUsersAsync(input);
        return result;
    }

    public async Task<PagedResult<UserDto>> GetAllUsersPagedAsync(GetAllUsersInput input)
    {
        var result = await _userRepository.GetAllUsersPagedAsync(input);
        return result;
    }

    public async Task CreateUserAsync(CreateUserDto input)
    {
        await _userRepository.CreateUserAsync(input);
    }

    public async Task UpdateUserAsync(UpdateUserDto input)
    {
        await _userRepository.UpdateUserAsync(input);
    }

    public async Task DeleteUsersAsync(int[] ids)
    {
        await _userRepository.DeleteUsersAsync(ids);
    }

    public async Task ChangeMyPassWordAsync(ChangeMyPasswordRequest input, int currentUserId)
    {
        await _userRepository.ChangeMyPassWordAsync(input, currentUserId);
    }

    public async Task SetPasswordAsync(SetPasswordRequest input)
    {
        await _userRepository.SetPasswordAsync(input);
    }

    public async Task ChangeEmailAsync(ChangeEmailRequest input)
    {
        await _userRepository.ChangeEmailAsync(input);
    }

    public async Task AssignRolesToUserAsync(AssignRolesToUserRequest input)
    {
        await _userRepository.AssignRolesToUserAsync(input);
    }

    public async Task<IList<string>> GetUserRolesAsync(int userId)
    {
        var result = await _userRepository.GetUserRolesAsync(userId);
        return result;
    }
}