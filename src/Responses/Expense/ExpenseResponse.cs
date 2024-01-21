using server.Models;

namespace server.Responses.Expense;

public class ExpenseResponse
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string? Note { get; set; }

    public int CategoryId { get; set; }

    public string CategoryType { get; set; }

    public int MethodId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

#nullable disable

    public Models.Category Category { get; set; }

    public Method Method { get; set; }

    public ExpenseResponse(Models.Expense expense)
    {
        Id = expense.Id;
        Amount = expense.Amount;
        Date = expense.Date;
        Note = expense.Note;
        CategoryId = expense.CategoryId;
        CategoryType = expense.CategoryType;
        MethodId = expense.MethodId;
        CreatedDate = expense.CreatedDate;
        UpdatedDate = expense.UpdatedDate;
    }
}