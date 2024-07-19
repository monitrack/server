namespace server.Responses.Expense;

public class ExpenseResponse
{
    public required int Id { get; set; }
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
    public required int CategoryId { get; set; }
    public required string CategoryType { get; set; }
    public required int AccountId { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }
    public string? Note { get; set; }
}