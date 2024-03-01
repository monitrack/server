using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos;
using server.Dtos.User;
using server.Extensions;
using server.Models;
using server.Responses.User;
using server.Services;

namespace Server.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEmailService _emailService;

    public AuthController(ApplicationDbContext dbContext, IEmailService emailService)
    {
        _dbContext = dbContext;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(CreateUserDto createUserDto)
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

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return Ok(user.MapToResponse());
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginUserDto userDto)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == userDto.Email);
        if (user is null)
        {
            return NotFound($"User with email {userDto.Email} not found!");
        }

        if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
        {
            return ValidationProblem("Wrong password!");
        }

        return Ok(user.MapToResponse());
    }

    [HttpGet("confirm-email/{userId}/{token}")]
    public async Task<ActionResult<UserResponse>> ConfirmEmail(string userId, string token)
    {
        User? user = await _dbContext.Users.SingleOrDefaultAsync(user => user.EmailConfirmationToken == token);
        if (user is null)
        {
            return NotFound("Invalid token!");
        }

        user.EmailConfirmedDate = DateTime.Now;
        await _dbContext.SaveChangesAsync();

        return Ok(user.MapToResponse());
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult<UserResponse>> ForgotPassword(string email)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(use => use.EmailConfirmationToken == email);
        if (user is null)
        {
            return NotFound($"User with email {email} not found!");
        }

        user.PasswordResetToken = "Token";
        user.EmailConfirmedDate = DateTime.Now;
        await _dbContext.SaveChangesAsync();

        // todo send to email

        return Ok(user.MapToResponse());
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<UserResponse>> ResetPassword(ResetUserPasswordDto userDto)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(
            use => use.PasswordResetToken == userDto.PasswordResetToken
        );
        if (user is null || user.PasswordResetTokenExpirationDate < DateTime.Now)
        {
            return NotFound("Invalid token!");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpirationDate = null;
        await _dbContext.SaveChangesAsync();

        return Ok(user.MapToResponse());
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

        await _emailService.SendAsync(emailDto);
    }
}