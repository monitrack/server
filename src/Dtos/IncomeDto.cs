namespace server.Dtos;

public class IncomeDto
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }
    
    public string? Note { get; set; }

    public string Category { get; set; } = null!;

    public required int MethodId { get; set; }
}