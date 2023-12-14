using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos;
using server.Models;
using server.Validators;

namespace Server.Controllers;

public class ExpenseController : ApiControllerBase
{
    private readonly MoniTrackContext _dbContext;

    public ExpenseController(MoniTrackContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> Create([FromBody] ExpenseDto expenseDto)
    {
        try
        {
            ExpenseDtoValidator validator = new ExpenseDtoValidator();
            await validator.ValidateAndThrowAsync(expenseDto);

            Expense expense = new Expense()
            {
                Amount = expenseDto.Amount,
                Date = expenseDto.Date,
                Note = expenseDto.Note,
                Category = expenseDto.Category,
                MethodId = expenseDto.MethodId
            };

            await _dbContext.Expenses.AddAsync(expense);
            await _dbContext.SaveChangesAsync();

            return Ok(expense);
        }
        catch (ValidationException exception)
        {
            return ValidationProblem(exception.Message);
        }
        catch (Exception exception)
        {
            return Problem("Something went wrong when creating expense. Exception message: " + exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExpenseDto>> Update(int id, [FromBody] ExpenseDto expenseDto)
    {
        try
        {
            ExpenseDtoValidator validator = new ExpenseDtoValidator();
            await validator.ValidateAndThrowAsync(expenseDto);

            Expense? expense = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);

            if (expense is null)
            {
                return NotFound();
            }

            expense.Amount = expenseDto.Amount;
            expense.Date = expenseDto.Date;
            expense.Note = expenseDto.Note;
            expense.Category = expenseDto.Category;
            expense.MethodId = expenseDto.MethodId;
            expense.UpdatedDate = DateTime.Now;

            _dbContext.Expenses.Update(expense);
            await _dbContext.SaveChangesAsync();

            return Ok(expense);
        }
        catch (ValidationException exception)
        {
            return ValidationProblem(exception.Message);
        }
        catch (Exception exception)
        {
            return Problem("Something went wrong when updating expense. Exception message: " + exception.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            Expense? expense = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);

            if (expense is null)
            {
                return NotFound();
            }

            _dbContext.Expenses.Remove(expense);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception exception)
        {
            return Problem("Something went wrong when deleting expense. Exception message: " + exception.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        try
        {
            Expense? expense = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
            
            if (expense is null)
            {
                return NotFound();
            }

            return Ok(expense);
        }
        catch (Exception exception)
        {
            return Problem("Something went wrong when getting expense. Exception message: " + exception.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expense>>> GetAll()
    {
        return await _dbContext.Expenses.ToListAsync();
    }
}