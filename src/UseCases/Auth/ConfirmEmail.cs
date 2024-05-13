using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Extensions;
using server.Models;
using server.Responses.User;

namespace server.UseCases.Auth;

public class ConfirmEmail(ApplicationDbContext dbContext)
{
    public async Task<UserResponse> Perform(int userId, string token)
    {
        User? user = await dbContext.Users.SingleOrDefaultAsync(user => user.EmailConfirmationToken == token && user.Id == userId);
        if (user is null)
        {
            throw new KeyNotFoundException("Invalid user confirmation token!");
        }

        user.EmailConfirmedDate = DateTime.Now;
        await dbContext.SaveChangesAsync();

        return user.MapToResponse();
    }
}