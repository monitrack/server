using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using server.Context;
using server.Dtos;
using server.Dtos.User;
using server.Extensions;
using server.Models;
using server.Responses.User;
using server.Services;

namespace server.UseCases.CreateUser;

public class CreateUser(ApplicationDbContext dbContext, IEmailService emailService)
{
    public async Task<UserResponse> Perform(CreateUserDto createUserDto)
    {
        User user = new()
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
            EmailConfirmationToken = "Token", // todo
            Accounts = new List<Account>
            {
                new()
                {
                    Name = "Main Account",
                    Balance = 0,
                    Currency = "USD",
                }
            }
        };

        SendEmailConfirmationAsync(user);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return user.MapToResponse();
    }

    private async void SendEmailConfirmationAsync(User user)
    {
        string body = "<h1>Welcome to MoniTrack!</h1>";
        body += "<div>Thank you for joining us!</div>";
        body += "<div>Please click the button below to confirm your email and complete the registration</div>";
        body += $"<a href='{HtmlEncoder.Default.Encode(user.EmailConfirmationToken)}'>clicking here</a>";

        EmailDto emailDto = new EmailDto
        {
            ToEmail = user.Email,
            Subject = "Welcome to MoniTrack!",
            Body = body
        };

        await emailService.SendAsync(emailDto);
    }
}