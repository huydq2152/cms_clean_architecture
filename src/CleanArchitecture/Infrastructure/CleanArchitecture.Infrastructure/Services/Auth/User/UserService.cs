using AutoMapper;
using CleanArchitecture.Application.Dtos.Auth.Users;
using CleanArchitecture.Application.Interfaces.Services.Auth.User;
using CleanArchitecture.Domain.Entities.Identity;
using Infrastructure.Common.Models.Paging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Services.Auth.User;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var result = _mapper.Map<AppUser, UserDto>(user);
        return result;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var result = _mapper.Map<IEnumerable<AppUser>, IEnumerable<UserDto>>(users);

        return result;
    }

    public async Task<PagedResult<UserDto>> GetAllUsersPagedAsync(UserPagingQueryInput input)
    {
        var query = _userManager.Users;
        if (!string.IsNullOrEmpty(input.Keyword))
        {
            query = query.Where(x => x.FirstName.Contains(input.Keyword)
                                     || x.UserName.Contains(input.Keyword)
                                     || x.Email.Contains(input.Keyword)
                                     || x.PhoneNumber.Contains(input.Keyword));
        }

        var objQuery = _mapper.ProjectTo<UserDto>(query);

        var result = await PagedResult<UserDto>.ToPagedList(objQuery, input.PageIndex, input.PageSize);
        return result;
    }

    public async Task CreateUserAsync(CreateUserDto input)
    {
        if ((await _userManager.FindByNameAsync(input.UserName)) != null)
        {
            throw new Exception("Username already exists");
        }

        if ((await _userManager.FindByEmailAsync(input.Email)) != null)
        {
            throw new Exception("Email already exists");
        }

        var user = _mapper.Map<CreateUserDto, AppUser>(input);
        await _userManager.CreateAsync(user, input.Password);
    }

    public async Task UpdateUserAsync(UpdateUserDto input)
    {
        var user = await _userManager.FindByIdAsync(input.Id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        _mapper.Map(input, user);
        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int[] ids)
    {
        foreach (var id in ids)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userManager.DeleteAsync(user);
        }
    }

    public async Task ChangeMyPassWord(ChangeMyPasswordRequest input, int currentUserId)
    {
        var user = await _userManager.FindByIdAsync(currentUserId.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);
    }

    public async Task SetPassword(SetPasswordRequest input)
    {
        var user = await _userManager.FindByIdAsync(input.CurrentUserId.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, input.NewPassword);
        await _userManager.UpdateAsync(user);
    }

    public async Task ChangeEmail(ChangeEmailRequest input)
    {
        var user = await _userManager.FindByIdAsync(input.CurrentUserId.ToString());    
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var token = await _userManager.GenerateChangeEmailTokenAsync(user, input.Email);
        await _userManager.ChangeEmailAsync(user, input.Email, token);
    }

    public async Task AssignRolesToUser(AssignRolesToUserRequest input)
    {
        var user = await _userManager.FindByIdAsync(input.CurrentUserId.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRolesAsync(user, input.Roles);
    }
}