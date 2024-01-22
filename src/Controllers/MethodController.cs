using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Method;
using server.Models;

namespace Server.Controllers;

public class MethodController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext; 
    
    public MethodController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Method>> Get(int id)
    {
        Method? method = await _dbContext.Methods.FirstOrDefaultAsync(x => x.Id == id);

        if (method is null)
        {
            return NotFound();
        }
        
        return Ok(method);
    }
    
    [HttpPost]
    public async Task<ActionResult<Method>> Create(CreateMethodDto createMethodDto)
    {
        Method newMethod = new Method
        {
            Name = createMethodDto.Name
        };

        await _dbContext.AddAsync(newMethod);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}