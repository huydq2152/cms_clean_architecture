using System.Security.Claims;
using CleanArchitecture.Application.Interfaces.Services.Auth.User;
using Contracts.Common.Models.Auth;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Services.Auth.User;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(UserClaims.Id);
            return userId != null ? Convert.ToInt32(userId) : null;
        }
    }
}