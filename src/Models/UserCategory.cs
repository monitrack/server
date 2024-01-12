namespace server.Models;

public class UserCategory : BaseEntity
{
    public required string Name { get; set; }

    public required int UserId { get; set; }
    
    public ICollection<Expense> Expenses { get; set; } = null!;

    public ICollection<Income> Incomes { get; set; } = null!;
}