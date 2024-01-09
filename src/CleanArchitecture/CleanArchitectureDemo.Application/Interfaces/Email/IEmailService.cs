using CleanArchitectureDemo.Application.DTOs.Email;

namespace CleanArchitectureDemo.Application.Interfaces.Email
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto request);
    }
}
