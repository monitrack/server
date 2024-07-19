using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Extensions;
using server.Models;
using server.Responses.User;

namespace server.UseCases.Auth;

public class ForgotPassword(ApplicationDbContext dbContext)
{
    public async Task<UserResponse> Perform(string email)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(user => user.EmailConfirmationToken == email);
        if (user is null)
        {
            throw new KeyNotFoundException($"User with email {email} not found!");
        }

        user.PasswordResetToken = "Token";
        user.EmailConfirmedDate = DateTime.Now;
        await dbContext.SaveChangesAsync();

        // todo send to email

        return user.MapToResponse();
    }
}