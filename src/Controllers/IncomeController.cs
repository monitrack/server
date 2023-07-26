using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos;
using server.Models;

namespace Server.Controllers;

[Route("api/[controller]")]
public class IncomeController : ApiControllerBase
{
    private MoniTrackContext _context; 
    
    public IncomeController()
    {
        _context = new MoniTrackContext();
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetAll()
    {
        // TODO: fetch incomes
        
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Income>> GetIncomeInfoById(int id)
    {
        Income? income = await _context.Incomes.FirstOrDefaultAsync(i => i.Id == id);

        if (income is null)
        {
            return NotFound();
        }
        
        return Ok(income);
    }

    [HttpPost]
    public async Task<ActionResult<IncomeDto>> Create([FromBody] IncomeDto incomeDto)
    {
        Income income = new Income()
        {
            Amount = incomeDto.Amount,
            Date = incomeDto.Date,
            Note = incomeDto.Note,
            Category = incomeDto.Category,
            MethodId = incomeDto.MethodId
        };

        await _context.Incomes.AddAsync(income);
        await _context.SaveChangesAsync();

        return Ok(income);
    }

    [HttpPut]
    public async Task<ActionResult<IncomeDto>> Update([FromBody] IncomeDto incomeDto)
    {
        // TODO: Update income
        
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Income? income = await _context.Incomes.FirstOrDefaultAsync(i => i.Id == id);

        if (income is null)
        {
            return NotFound();
        }
        
        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync();

        return Ok();
    }
}