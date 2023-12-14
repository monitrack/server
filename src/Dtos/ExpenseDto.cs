namespace server.Dtos;

public class ExpenseDto
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }

    public string? Note { get; set; } = null;

    public string Category { get; set; } = null!;

    public required int MethodId { get; set; }
}