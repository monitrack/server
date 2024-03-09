using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.User;
using server.Extensions;
using server.Models;
using server.Responses.User;
using server.UseCases.CreateUser;

namespace Server.Controllers;

public class AuthController(ApplicationDbContext dbContext, CreateUser createUser) : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(CreateUserDto createUserDto)
    {
        var response = await createUser.Perform(createUserDto);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginUserDto userDto)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == userDto.Email);
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
        User? user = await dbContext.Users.SingleOrDefaultAsync(user => user.EmailConfirmationToken == token);
        if (user is null)
        {
            return NotFound("Invalid token!");
        }

        user.EmailConfirmedDate = DateTime.Now;
        await dbContext.SaveChangesAsync();

        return Ok(user.MapToResponse());
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult<UserResponse>> ForgotPassword(string email)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(use => use.EmailConfirmationToken == email);
        if (user is null)
        {
            return NotFound($"User with email {email} not found!");
        }

        user.PasswordResetToken = "Token";
        user.EmailConfirmedDate = DateTime.Now;
        await dbContext.SaveChangesAsync();

        // todo send to email

        return Ok(user.MapToResponse());
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<UserResponse>> ResetPassword(ResetUserPasswordDto userDto)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(
            use => use.PasswordResetToken == userDto.PasswordResetToken
        );
        if (user is null || user.PasswordResetTokenExpirationDate < DateTime.Now)
        {
            return NotFound("Invalid token!");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpirationDate = null;
        await dbContext.SaveChangesAsync();

        return Ok(user.MapToResponse());
    }
}