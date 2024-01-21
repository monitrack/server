using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Income;
using server.Models;
using server.Responses.Income;

namespace Server.Controllers;

public class IncomeController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public IncomeController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<IncomeResponse>> Create(CreateIncomeDto createIncomeDto)
    {
        DateTime now = DateTime.Now;
        Income income = new Income
        {
            Amount = createIncomeDto.Amount,
            Date = createIncomeDto.Date,
            Note = createIncomeDto.Note,
            CategoryId = createIncomeDto.CategoryId,
            CategoryType = createIncomeDto.CategoryType,
            MethodId = createIncomeDto.MethodId,
            CreatedDate = now,
            UpdatedDate = now,
        };

        _dbContext.Incomes.Add(income);
        await _dbContext.SaveChangesAsync();

        return Ok(new IncomeResponse(income));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IncomeResponse>> Update(int id, UpdateIncomeDto updateIncomeDto)
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

        return Ok(new IncomeResponse(income));
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
    public async Task<ActionResult<IncomeResponse>> GetById(int id)
    {
        Income? income = await _dbContext.Incomes.FindAsync(id);
        if (income is null)
        {
            return NotFound($"Income with id {id} not found!");
        }

        return Ok(new IncomeResponse(income));
    }

    [HttpGet]
    public async Task<ActionResult<List<IncomeResponse>>> GetAll()
    {
        List<Income> incomes = await _dbContext.Incomes.ToListAsync();
        List<IncomeResponse> responses = incomes.Select(income => new IncomeResponse(income)).ToList();
        
        return Ok(responses);
    }
}