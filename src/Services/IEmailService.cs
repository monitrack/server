using server.Dtos;

namespace server.Services;

public interface IEmailService
{
    Task SendAsync(EmailDto emailDto);
}