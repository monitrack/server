using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.User;
using server.Extensions;
using server.Models;
using server.Responses.User;

namespace server.UseCases.Auth;

public class LoginUser(ApplicationDbContext dbContext)
{
    public async Task<UserResponse> Perform(LoginUserDto loginUserDto)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == loginUserDto.Email);
        if (user is null)
        {
            throw new KeyNotFoundException($"User with email {loginUserDto.Email} not found!");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
        {
            throw new AuthenticationException("Wrong password!");
        }

        // todo login user
        
        return user.MapToResponse();
    }
}