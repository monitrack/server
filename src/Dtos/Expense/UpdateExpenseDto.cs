namespace server.Dtos.Expense;

public class UpdateExpenseDto
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }

    public string? Note { get; set; } = null;

    public required int CategoryId { get; set; }

    public required string CategoryType { get; set; }

    public required int MethodId { get; set; }
}