namespace CleanArchitecture.Application.Interfaces.Services.Auth.User;

public interface ICurrentUserService
{
    int? UserId { get; }
}