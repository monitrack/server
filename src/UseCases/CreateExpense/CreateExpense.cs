using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Expense;
using server.Extensions;
using server.Models;
using server.Responses.Expense;

namespace server.UseCases.CreateExpense;

public class CreateExpense(ApplicationDbContext dbContext)
{
    public async Task<ExpenseResponse> Perform(CreateExpenseDto createExpenseDto)
    {
        int accountId = createExpenseDto.AccountId;
        Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        if (account is null)
        { // todo find out how to solve
            return NotFound($"Account by id {accountId} not found!");
        }

        account.Balance -= createExpenseDto.Amount;
        
        Expense expense = new Expense
        {
            Amount = createExpenseDto.Amount,
            Date = createExpenseDto.Date,
            Note = createExpenseDto.Note,
            CategoryId = createExpenseDto.CategoryId,
            CategoryType = createExpenseDto.CategoryType,
            AccountId = accountId,
        };

        dbContext.Expenses.Add(expense);
        await dbContext.SaveChangesAsync();

        return expense.MapToResponse();
    }
}