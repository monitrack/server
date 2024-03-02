using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Expense;
using server.Models;
using server.Responses.Expense;

namespace Server.Controllers;

public class ExpenseController(ApplicationDbContext dbContext) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ExpenseResponse>> Create(CreateExpenseDto createExpenseDto)
    {
        int accountId = createExpenseDto.AccountId;
        // Todo: Extract into class
        Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        if (account is null)
        {
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

        return Ok(new ExpenseResponse(expense));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExpenseResponse>> Update(int id, UpdateExpenseDto updateExpenseDto)
    {
        Expense? expense = await dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense is null)
        {
            return NotFound($"Expense with id {id} not found!");
        }

        int accountId = updateExpenseDto.AccountId;
        // Todo: Extract into class
        Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        if (account is null)
        {
            return NotFound($"Account by id {accountId} not found!");
        }

        decimal amountDifference = updateExpenseDto.Amount - expense.Amount;
        account.Balance -= amountDifference;

        expense.Amount = updateExpenseDto.Amount;
        expense.Date = updateExpenseDto.Date;
        expense.Note = updateExpenseDto.Note;
        expense.CategoryId = updateExpenseDto.CategoryId;
        expense.CategoryType = updateExpenseDto.CategoryType;
        expense.AccountId = accountId;
        expense.UpdatedDate = DateTime.Now;

        await dbContext.SaveChangesAsync();

        return Ok(new ExpenseResponse(expense));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Expense? expense = await dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense is null)
        {
            return NotFound($"Expense with id {id} not found!");
        }

        // Todo: Extract into class
        int accountId = expense.AccountId;
        Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        if (account is null)
        {
            return NotFound($"Account by id {accountId} not found!");
        }

        account.Balance += expense.Amount;

        dbContext.Expenses.Remove(expense);
        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseResponse>> GetById(int id)
    {
        Expense? expense = await dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense is null)
        {
            return NotFound($"Expense with id {id} not found!");
        }

        return Ok(new ExpenseResponse(expense));
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseResponse>>> GetAll()
    {
        List<Expense> expenses = await dbContext.Expenses.ToListAsync();
        List<ExpenseResponse> responses = expenses.Select(expense => new ExpenseResponse(expense)).ToList();
        
        return Ok(responses);
    }
}