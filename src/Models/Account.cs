namespace server.Models;

public class Account : BaseEntity
{
    public required string Name { get; set; } 

    public required decimal Balance { get; set; } 

    public required string Currency { get; set; }

    public string? Description { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}