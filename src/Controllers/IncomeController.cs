using Microsoft.AspNetCore.Mvc;
using server.Dtos;

namespace Server.Controllers;

[Route("api/[controller]")]
public class IncomeController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetAll()
    {
        // TODO: fetch incomes
        
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IncomeDto>> GetIncomeInfoById(int id)
    {
        // TODO: fetch income by id
        
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<IncomeDto>> Create([FromBody] IncomeDto incomeDto)
    {
        // TODO: Create income
        
        throw new NotImplementedException();
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
        // TODO: Delete the income
        
        // maybe I will surround it by try catch
        
        // Income income = await _context.Incomes.FindAsync(id);
        // await  _context.Incomes.RemoveAsync(income);
        // await _context.saveChangesAsync();
        
        throw new NotImplementedException();
    }
}