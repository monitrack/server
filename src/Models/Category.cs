namespace server.Models;

public class Category : BaseEntity
{
    public required string Name { get; set; }

    public ICollection<Expense> Expenses { get; set; } = null!;

    public ICollection<Income> Incomes { get; set; } = null!;
}