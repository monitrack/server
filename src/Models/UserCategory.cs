namespace server.Models;

public class UserCategory : BaseEntity
{
    public required string Name { get; set; }

    public required int UserId { get; set; }
}