using Contracts.Common.Models.Email;

namespace Contracts.Services;

public interface ISmtpEmailService: IEmailService<MailRequest>
{
    
}