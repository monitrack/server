using server.Models;
using server.Responses.User;

namespace server.Extensions;

public static class UserExtensions
{
    public static UserResponse MapToResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedDate = user.CreatedDate,
            UpdatedDate = user.UpdatedDate,
        };
    }
}