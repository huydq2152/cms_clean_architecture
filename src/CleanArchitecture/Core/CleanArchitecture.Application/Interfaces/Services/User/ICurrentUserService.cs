namespace CleanArchitecture.Application.Interfaces.Services.User;

public interface ICurrentUserService
{
    int? UserId { get; }
}