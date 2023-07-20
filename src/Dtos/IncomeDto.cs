namespace server.Dtos;

public class IncomeDto
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }
    
    public string? Note { get; set; }

    public required string Category { get; set; }

    public required string Account { get; set; }
}