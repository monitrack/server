namespace server.Responses.User;

public class UserResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public UserResponse(Models.User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        CreatedDate = user.CreatedDate;
        UpdatedDate = user.UpdatedDate;
    }
}