namespace server.Dtos.Income;

public class CreateIncomeDto
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }
    
    public string? Note { get; set; }

    public required int CategoryId { get; set; }

    public required string CategoryType { get; set; }

    public required int MethodId { get; set; }
}