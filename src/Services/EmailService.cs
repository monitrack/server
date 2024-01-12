using MailKit.Net.Smtp;
using MailKit.Security;
using server.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using server.Dtos;

namespace server.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendAsync(EmailDto emailDto)
    {
        MimeMessage mimeMessage = new MimeMessage();
        mimeMessage.Sender = MailboxAddress.Parse(_emailSettings.Email);
        mimeMessage.To.Add(MailboxAddress.Parse(emailDto.ToEmail));
        mimeMessage.Subject = emailDto.Subject;
        BodyBuilder bodyBuilder = new BodyBuilder { HtmlBody = emailDto.Body };
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        using SmtpClient smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);
        await smtpClient.SendAsync(mimeMessage);
        await smtpClient.DisconnectAsync(true);
    }
}