using server.Models;
using server.Responses.Expense;

namespace server.Extensions;

public static class ExpenseExtensions
{
    public static ExpenseResponse MapToResponse(this Expense expense)
    {
        return new ExpenseResponse
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Date = expense.Date,
            Note = expense.Note,
            CategoryId = expense.CategoryId,
            CategoryType = expense.CategoryType,
            AccountId = expense.AccountId,
            CreatedDate = expense.CreatedDate,
            UpdatedDate = expense.UpdatedDate,
        };
    }
}