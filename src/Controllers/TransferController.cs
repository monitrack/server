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
        Transfer transfer = new()
        {
            Amount = transferDto.Amount,
            AccountFromId = transferDto.AccountFromId,
            AccountToId = transferDto.AccountToId,
            Description = transferDto.Description,
            Date = transferDto.Date
        };

        dbContext.Transfers.Add(transfer);
        await dbContext.SaveChangesAsync();

        return Ok(transfer.MapToResponse());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTransferDto transferDto)
    {
        Transfer? transfer = await dbContext.Transfers.FirstOrDefaultAsync(t => t.Id == id);
        if (transfer is null)
        {
            return NotFound($"Transfer with id {id} not found!");
        }

        // todo update account balance
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
        int rows = await dbContext.Transfers.Where(t => t.Id == id).ExecuteDeleteAsync();
        if (rows == 0)
        {
            return NotFound($"Transfer with id {id} not found!");
        }

        // todo update account balance

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