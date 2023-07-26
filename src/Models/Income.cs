namespace server.Models;

public class Income : BaseEntity
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }

    public string? Note { get; set; }
    
    public string Category { get; set; } = null!;
    
    public int MethodId { get; set; }

    public Method Method { get; set; } = null!;
}