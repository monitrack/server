using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Expense;
using server.Models;
using server.Validators;

namespace Server.Controllers;

public class ExpenseController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ExpenseController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<Expense>> Create(CreateExpenseDto createExpenseDto)
    {
        Expense expense = new Expense
        {
            Amount = createExpenseDto.Amount,
            Date = createExpenseDto.Date,
            Note = createExpenseDto.Note,
            CategoryId = createExpenseDto.CategoryId,
            CategoryType = createExpenseDto.CategoryType,
            MethodId = createExpenseDto.MethodId,
        };

        ExpenseValidator validator = new ExpenseValidator();
        ValidationResult validationResult = await validator.ValidateAsync(expense);
        if (!validationResult.IsValid)
        {
            return ValidationProblem(validationResult.ToString());
        }

        await _dbContext.Expenses.AddAsync(expense);
        await _dbContext.SaveChangesAsync();

        return Ok(expense);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Expense>> Update(int id, UpdateExpenseDto updateExpenseDto)
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

        ExpenseValidator validator = new ExpenseValidator();
        ValidationResult validationResult = await validator.ValidateAsync(expense);
        if (!validationResult.IsValid)
        {
            return ValidationProblem(validationResult.ToString());
        }

        _dbContext.Expenses.Update(expense);
        await _dbContext.SaveChangesAsync();

        return Ok(expense);
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
    public async Task<ActionResult<Expense>> GetById(int id)
    {
        Expense? expense = await _dbContext.Expenses.FindAsync(id);
        if (expense is null)
        {
            return NotFound($"Expense with id {id} not found!");
        }

        return Ok(expense);
    }

    [HttpGet]
    public async Task<ActionResult<List<Expense>>> GetAll()
    {
        return Ok(await _dbContext.Expenses.ToListAsync());
    }
}