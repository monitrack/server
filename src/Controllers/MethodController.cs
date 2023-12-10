using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Models;

namespace Server.Controllers;

public class MethodController : ApiControllerBase
{
    private MoniTrackContext _context; 
    
    public MethodController()
    {
        _context = new MoniTrackContext();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Method>> Get(int id)
    {
        Method? method = await _context.Method.FirstOrDefaultAsync(x => x.Id == id);

        if (method is null)
        {
            return NotFound();
        }
        
        return Ok(method);
    }
    
    [HttpPost]
    public async Task<ActionResult<Method>> Create([FromBody] Method method)
    {
        Method newMethod = new Method()
        {
            Name = method.Name
        };
        
        await _context.AddAsync(newMethod);
        await _context.SaveChangesAsync();

        return Ok();
    }
}