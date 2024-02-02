using System.Security.Claims;
using CleanArchitecture.Application.Interfaces.Services.User;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Services.User;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) != null
        ? Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier))
        : null;
}