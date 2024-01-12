using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.UserCategory;
using server.Models;

namespace Server.Controllers;

public class UserCategoryController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public UserCategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<UserCategory>> Create(CreateUserCategoryDto userCategoryDto)
    {
        DateTime now = DateTime.Now;
        UserCategory userCategory = new UserCategory
        {
            UserId = userCategoryDto.UserId,
            Name = userCategoryDto.Name,
            CreatedDate = now,
            UpdatedDate = now
        };

        await _dbContext.UserCategories.AddAsync(userCategory);
        await _dbContext.SaveChangesAsync();

        return Ok(userCategory);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserCategory>> Update(int id, UpdateUserCategoryDto userCategoryDto)
    {
        UserCategory? userCategory = await _dbContext.UserCategories.FindAsync(id);
        if (userCategory is null)
        {
            return NotFound($"User category with id {id} not found!");
        }

        userCategory.Name = userCategoryDto.Name;
        userCategory.UpdatedDate = DateTime.Now;

        _dbContext.UserCategories.Update(userCategory);
        await _dbContext.SaveChangesAsync();

        return Ok(userCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        UserCategory? userCategory = await _dbContext.UserCategories.FindAsync(id);
        if (userCategory is null)
        {
            return NotFound($"User category with id {id} not found!");
        }

        _dbContext.UserCategories.Remove(userCategory);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserCategory>> GetById(int id)
    {
        UserCategory? userCategory = await _dbContext.UserCategories.FindAsync(id);
        if (userCategory is null)
        {
            return NotFound($"User category with id {id} not found!");
        }

        return Ok(userCategory);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserCategory>>> GetAll()
    {
        return Ok(await _dbContext.UserCategories.ToListAsync());
    }
}