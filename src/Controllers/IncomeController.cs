using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Income;
using server.Models;

namespace Server.Controllers;

public class IncomeController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public IncomeController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<CreateIncomeDto>> Create(CreateIncomeDto createIncomeDto)
    {
        Income income = new Income
        {
            Amount = createIncomeDto.Amount,
            Date = createIncomeDto.Date,
            Note = createIncomeDto.Note,
            CategoryId = createIncomeDto.CategoryId,
            CategoryType = createIncomeDto.CategoryType,
            MethodId = createIncomeDto.MethodId
        };

        await _dbContext.Incomes.AddAsync(income);
        await _dbContext.SaveChangesAsync();

        return Ok(income);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CreateIncomeDto>> Update(int id, UpdateIncomeDto updateIncomeDto)
    {
        Income? income = await _dbContext.Incomes.FindAsync(id);
        if (income is null)
        {
            return NotFound($"Income with id {id} not found!");
        }

        income.Amount = updateIncomeDto.Amount;
        income.Date = updateIncomeDto.Date;
        income.Note = updateIncomeDto.Note;
        income.CategoryId = updateIncomeDto.CategoryId;
        income.CategoryType = updateIncomeDto.CategoryType;
        income.MethodId = updateIncomeDto.MethodId;
        income.UpdatedDate = DateTime.Now;

        _dbContext.Incomes.Update(income);
        await _dbContext.SaveChangesAsync();

        return Ok(income);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Income? income = await _dbContext.Incomes.FirstOrDefaultAsync(i => i.Id == id);

        if (income is null)
        {
            return NotFound();
        }

        _dbContext.Incomes.Remove(income);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Income>> GetById(int id)
    {
        Income? income = await _dbContext.Incomes.FindAsync(id);
        if (income is null)
        {
            return NotFound($"Income with id {id} not found!");
        }

        return Ok(income);
    }

    [HttpGet]
    public async Task<ActionResult<List<Income>>> GetAll()
    {
        return Ok(await _dbContext.Incomes.ToListAsync());
    }
}