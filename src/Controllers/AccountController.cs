using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Account;
using server.Models;
using server.Responses.Account;

namespace Server.Controllers;

public class AccountController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public AccountController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<AccountResponse>> Create(CreateAccountDto createAccountDto)
    {
        DateTime now = DateTime.Now;
        Account account = new Account
        {
            Name = createAccountDto.Name,
            Balance = createAccountDto.Balance,
            Currency = createAccountDto.Currency,
            Description = createAccountDto.Description,
            UserId = createAccountDto.UserId,
            CreatedDate = now,
            UpdatedDate = now,
        };

        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync();

        return Ok(new AccountResponse(account));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AccountResponse>> Update(int id, UpdateAccountDto updateAccountDto)
    {
        Account? account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        if (account is null)
        {
            return NotFound($"Account with id {id} not found!");
        }

        account.Name = updateAccountDto.Name;
        account.Balance = updateAccountDto.Balance;
        account.Currency = updateAccountDto.Currency;
        account.Description = updateAccountDto.Description;
        account.UpdatedDate = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return Ok(new AccountResponse(account));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        int rows = await _dbContext.Accounts.Where(a => a.Id == id).ExecuteDeleteAsync();
        if (rows == 0)
        {
            return NotFound($"Account with id {id} not found!");
        }

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountResponse>> GetById(int id)
    {
        Account? account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        if (account is null)
        {
            return NotFound($"Account with id {id} not found!");
        }

        return Ok(new AccountResponse(account));
    }

    [HttpGet("users/accounts")]
    public async Task<ActionResult<List<AccountResponse>>> GetUserAccounts(int userId)
    {
        return await _dbContext.Accounts
            .Where(account => account.UserId == userId)
            .Select(account => new AccountResponse(account))
            .ToListAsync();
    }
}