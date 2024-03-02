using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Income;
using server.Models;
using server.Responses.Income;

namespace Server.Controllers;

public class IncomeController(ApplicationDbContext dbContext) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IncomeResponse>> Create(CreateIncomeDto createIncomeDto)
    {
        int accountId = createIncomeDto.AccountId;
        // Todo: Extract into class
        Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        if (account is null)
        {
            return NotFound($"Account by id {accountId} not found!");
        }

        account.Balance += createIncomeDto.Amount;

        Income income = new Income
        {
            Amount = createIncomeDto.Amount,
            Date = createIncomeDto.Date,
            Note = createIncomeDto.Note,
            CategoryId = createIncomeDto.CategoryId,
            CategoryType = createIncomeDto.CategoryType,
            AccountId = createIncomeDto.AccountId,
        };

        dbContext.Incomes.Add(income);
        await dbContext.SaveChangesAsync();

        return Ok(new IncomeResponse(income));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IncomeResponse>> Update(int id, UpdateIncomeDto updateIncomeDto)
    {
        Income? income = await dbContext.Incomes.FirstOrDefaultAsync(i => i.Id == id);
        if (income is null)
        {
            return NotFound($"Income with id {id} not found!");
        }

        // Todo: Extract into class
        int accountId = updateIncomeDto.AccountId;
        Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        if (account is null)
        {
            return NotFound($"Account by id {accountId} not found!");
        }

        account.Balance -= updateIncomeDto.Amount;
        decimal amountDifference = updateIncomeDto.Amount - income.Amount;
        account.Balance += amountDifference;

        income.Amount = updateIncomeDto.Amount;
        income.Date = updateIncomeDto.Date;
        income.Note = updateIncomeDto.Note;
        income.CategoryId = updateIncomeDto.CategoryId;
        income.CategoryType = updateIncomeDto.CategoryType;
        income.AccountId = updateIncomeDto.AccountId;
        income.UpdatedDate = DateTime.Now;

        await dbContext.SaveChangesAsync();

        return Ok(new IncomeResponse(income));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Income? income = await dbContext.Incomes.FirstOrDefaultAsync(i => i.Id == id);
        if (income is null)
        {
            return NotFound();
        }

        // Todo: Extract into class
        int accountId = income.AccountId;
        Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
        if (account is null)
        {
            return NotFound($"Account by id {accountId} not found!");
        }

        account.Balance -= income.Amount;

        dbContext.Incomes.Remove(income);
        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncomeResponse>> GetById(int id)
    {
        Income? income = await dbContext.Incomes.FirstOrDefaultAsync(i => i.Id == id);
        if (income is null)
        {
            return NotFound($"Income with id {id} not found!");
        }

        return Ok(new IncomeResponse(income));
    }

    [HttpGet]
    public async Task<ActionResult<List<IncomeResponse>>> GetAll()
    {
        List<Income> incomes = await dbContext.Incomes.ToListAsync();
        List<IncomeResponse> responses = incomes.Select(income => new IncomeResponse(income)).ToList();
        
        return Ok(responses);
    }
}