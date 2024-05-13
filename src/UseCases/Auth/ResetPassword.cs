using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.User;
using server.Extensions;
using server.Models;
using server.Responses.User;

namespace server.UseCases.Auth;

public class ResetPassword(ApplicationDbContext dbContext)
{
    public async Task<UserResponse> Perform(ResetUserPasswordDto userDto)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(
            user => user.PasswordResetToken == userDto.PasswordResetToken
        );
        if (user is null || user.PasswordResetTokenExpirationDate < DateTime.Now)
        {
            throw new AuthenticationException("Token either invalid or expired!");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpirationDate = null;
        await dbContext.SaveChangesAsync();

        return user.MapToResponse();
    }
}