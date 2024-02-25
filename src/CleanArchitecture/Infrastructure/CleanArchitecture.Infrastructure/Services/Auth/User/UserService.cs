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

    public async Task<IdentityResult> CreateUserAsync(CreateUserDto input)
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
        var result = await _userManager.CreateAsync(user, input.Password);
        return result;
    }

    public async Task<IdentityResult> UpdateUserAsync(UpdateUserDto input)
    {
        var user = await _userManager.FindByIdAsync(input.Id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }
        _mapper.Map(input, user);
        var result = await _userManager.UpdateAsync(user);

        return result;
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

    public async Task<IdentityResult> ChangeMyPassWord(ChangeMyPasswordRequest input)
    {
        var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var result = await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);
        return result;
    }

    public async Task<IdentityResult> SetPassword(SetPasswordRequest input)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> ChangeEmail(ChangeEmailRequest input)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> AssignRolesToUser(AssignRolesToUserRequest input)
    {
        throw new NotImplementedException();
    }
}