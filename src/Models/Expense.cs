namespace server.Models;

public class Expense : BaseEntity
{
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
    public string? Note { get; set; }
    public required int CategoryId { get; set; }
    public required string CategoryType { get; set; }
    public int AccountId { get; set; }

    #nullable disable
    public Account Account { get; set; }
}