namespace server.Models;

public class Income : BaseEntity
{
    public decimal Amount { get; set; }

    public required DateTime Date { get; set; }
    
    public string? Note { get; set; }

    public required string Method { get; set; }
}