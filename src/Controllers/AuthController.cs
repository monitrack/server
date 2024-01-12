using System.Text.Encodings.Web;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos;
using server.Dtos.User;
using server.Models;
using server.Services;
using server.Validators;

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
    public async Task<ActionResult<User>> Register(CreateUserDto createUserDto)
    {
        User user = new User
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
            EmailConfirmationToken = "Token" // todo 
        };

        UserValidator validator = new UserValidator();
        ValidationResult validationResult = await validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return ValidationProblem(validationResult.ToString());
        }

        SendEmailConfirmation(user);

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(LoginUserDto userDto)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == userDto.Email);
        if (user == null)
        {
            return NotFound($"User with email {userDto.Email} not found!");
        }

        if (BCrypt.Net.BCrypt.EnhancedVerify(userDto.Password, user.PasswordHash))
        {
            return ValidationProblem("Wrong password!");
        }

        return Ok(user);
    }

    [HttpGet("confirm-email/{userId}/{token}")]
    public async Task<ActionResult<User>> ConfirmEmail(string userId, string token)
    {
        User? user = await _dbContext.Users.SingleOrDefaultAsync(user => user.EmailConfirmationToken == token);
        if (user == null)
        {
            return NotFound("Invalid token!");
        }

        user.EmailConfirmedDate = DateTime.Now;
        await _dbContext.SaveChangesAsync();

        return Ok(user);
    }
    
    [HttpPost("forgot-password")]
    public async Task<ActionResult<User>> ForgotPassword(string email)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(use => use.EmailConfirmationToken == email);
        if (user == null)
        {
            return NotFound($"User with email {email} not found!");
        }

        user.PasswordResetToken = "Token";
        user.EmailConfirmedDate = DateTime.Now;
        await _dbContext.SaveChangesAsync();
        
        // todo send to email

        return Ok(user);
    }
    
    [HttpPost("reset-password")]
    public async Task<ActionResult<User>> ResetPassword(ResetUserPasswordDto userDto)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(use => use.PasswordResetToken == userDto.PasswordResetToken);
        if (user == null || user.PasswordResetTokenExpirationDate < DateTime.Now)
        {
            return NotFound("Invalid token!");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpirationDate = null;
        await _dbContext.SaveChangesAsync();

        return Ok(user);
    }

    private async void SendEmailConfirmation(User user)
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