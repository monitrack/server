using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Transfer;
using server.Extensions;
using server.Models;

namespace Server.Controllers;

public class TransferController(ApplicationDbContext dbContext) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create(CreateTransferDto transferDto)
    {
        decimal transferAmount = transferDto.Amount;
        int accountFromId = transferDto.AccountFromId;
        int accountToId = transferDto.AccountToId;

        Transfer transfer = new()
        {
            Amount = transferDto.Amount,
            AccountFromId = transferDto.AccountFromId,
            AccountToId = transferDto.AccountToId,
            Description = transferDto.Description,
            Date = transferDto.Date
        };

        Account? accountFrom = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountFromId);
        if (accountFrom is null)
        {
            return NotFound($"Account from by id {accountFromId} not found!");
        }

        Account? accountTo = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountToId);
        if (accountTo is null)
        {
            return NotFound($"Account to by id {accountTo} not found!");
        }

        accountFrom.Balance -= transferAmount;
        accountTo.Balance += transferAmount;

        dbContext.Transfers.Add(transfer);
        await dbContext.SaveChangesAsync();

        return Ok(transfer.MapToResponse());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTransferDto transferDto)
    {
        decimal newTransferAmount = transferDto.Amount;
        int accountFromId = transferDto.AccountFromId;
        int accountToId = transferDto.AccountToId;

        Transfer? transfer = await dbContext.Transfers.FirstOrDefaultAsync(t => t.Id == id);
        if (transfer is null)
        {
            return NotFound($"Transfer with id {id} not found!");
        }

        Account? accountFrom = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountFromId);
        if (accountFrom is null)
        {
            return NotFound($"Account from by id {accountFromId} not found!");
        }

        Account? accountTo = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountToId);
        if (accountTo is null)
        {
            return NotFound($"Account to by id {accountTo} not found!");
        }

        if (newTransferAmount > transfer.Amount)
        {
            accountFrom.Balance -= newTransferAmount;
            accountTo.Balance += newTransferAmount;
        }
        else
        {
            accountFrom.Balance += newTransferAmount;
            accountTo.Balance -= newTransferAmount;
        }

        // todo validate account ids (user should only transfer to his accounts)
        // todo if user changes accounts, then we should update account balance    

        transfer.Amount = transferDto.Amount;
        transfer.AccountFromId = transferDto.AccountFromId;
        transfer.AccountToId = transferDto.AccountToId;
        transfer.Date = transferDto.Date;
        transfer.Description = transferDto.Description;

        await dbContext.SaveChangesAsync();

        return Ok(transfer.MapToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Transfer? transfer = await dbContext.Transfers.FirstOrDefaultAsync(t => t.Id == id);
        if (transfer == null)
        {
            return NotFound($"Transfer with id {id} not found!");
        }

        decimal transferAmount = transfer.Amount;

        Account? accountFrom = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == transfer.AccountFromId);
        if (accountFrom is null)
        {
            return NotFound($"Account from by id {transfer.AccountFromId} not found!");
        }

        Account? accountTo = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == transfer.AccountToId);
        if (accountTo is null)
        {
            return NotFound($"Account from by id {accountTo} not found!");
        }

        accountFrom.Balance -= transfer.Amount;
        accountTo.Balance += transferAmount;

        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Transfer? transfer = await dbContext.Transfers.FirstOrDefaultAsync(t => t.Id == id);
        if (transfer == null)
        {
            return NotFound($"Transfer with id {id} not found!");
        }

        return Ok(transfer.MapToResponse());
    }
}