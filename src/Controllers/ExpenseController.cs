using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Expense;
using server.Models;
using server.Responses.Expense;

namespace Server.Controllers;

public class ExpenseController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ExpenseController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseResponse>> Create(CreateExpenseDto createExpenseDto)
    {
        DateTime now = DateTime.Now;
        Expense expense = new Expense
        {
            Amount = createExpenseDto.Amount,
            Date = createExpenseDto.Date,
            Note = createExpenseDto.Note,
            CategoryId = createExpenseDto.CategoryId,
            CategoryType = createExpenseDto.CategoryType,
            MethodId = createExpenseDto.MethodId,
            CreatedDate = now,
            UpdatedDate = now,
        };

        _dbContext.Expenses.Add(expense);
        await _dbContext.SaveChangesAsync();

        return Ok(new ExpenseResponse(expense));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExpenseResponse>> Update(int id, UpdateExpenseDto updateExpenseDto)
    {
        Expense? expense = await _dbContext.Expenses.FindAsync(id);
        if (expense is null)
        {
            return NotFound($"Expense with id {id} not found!");
        }

        expense.Amount = updateExpenseDto.Amount;
        expense.Date = updateExpenseDto.Date;
        expense.Note = updateExpenseDto.Note;
        expense.CategoryId = updateExpenseDto.CategoryId;
        expense.CategoryType = updateExpenseDto.CategoryType;
        expense.MethodId = updateExpenseDto.MethodId;
        expense.UpdatedDate = DateTime.Now;

        _dbContext.Expenses.Update(expense);
        await _dbContext.SaveChangesAsync();

        return Ok(new ExpenseResponse(expense));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Expense? expense = await _dbContext.Expenses.FindAsync(id);
        if (expense is null)
        {
            return NotFound($"Expense with id {id} not found!");
        }

        _dbContext.Expenses.Remove(expense);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseResponse>> GetById(int id)
    {
        Expense? expense = await _dbContext.Expenses.FindAsync(id);
        if (expense is null)
        {
            return NotFound($"Expense with id {id} not found!");
        }

        return Ok(new ExpenseResponse(expense));
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseResponse>>> GetAll()
    {
        List<Expense> expenses = await _dbContext.Expenses.ToListAsync();
        List<ExpenseResponse> responses = expenses.Select(expense => new ExpenseResponse(expense)).ToList();
        
        return Ok(responses);
    }
}