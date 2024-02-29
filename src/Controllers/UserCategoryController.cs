using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.UserCategory;
using server.Models;
using server.Responses.UserCategory;

namespace Server.Controllers;

public class UserCategoryController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public UserCategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<UserCategoryResponse>> Create(CreateUserCategoryDto userCategoryDto)
    {
        DateTime now = DateTime.Now;
        UserCategory userCategory = new UserCategory
        {
            UserId = userCategoryDto.UserId,
            Name = userCategoryDto.Name,
            CreatedDate = now,
            UpdatedDate = now
        };

        _dbContext.UserCategories.Add(userCategory);
        await _dbContext.SaveChangesAsync();

        return Ok(new UserCategoryResponse(userCategory));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserCategoryResponse>> Update(int id, UpdateUserCategoryDto userCategoryDto)
    {
        UserCategory? userCategory = await _dbContext.UserCategories.FirstOrDefaultAsync(u => u.Id == id);
        if (userCategory is null)
        {
            return NotFound($"User category with id {id} not found!");
        }

        userCategory.Name = userCategoryDto.Name;
        userCategory.UpdatedDate = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return Ok(new UserCategoryResponse(userCategory));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        int rows = await _dbContext.UserCategories.Where(u => u.Id == id).ExecuteDeleteAsync();
        if (rows == 0)
        {
            return NotFound($"User category with id {id} not found!");
        }

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserCategoryResponse>> GetById(int id)
    {
        UserCategory? userCategory = await _dbContext.UserCategories.FirstOrDefaultAsync(u => u.Id == id);
        if (userCategory is null)
        {
            return NotFound($"User category with id {id} not found!");
        }

        return Ok(new UserCategoryResponse(userCategory));
    }

    [HttpGet]
    public async Task<ActionResult<List<UserCategoryResponse>>> GetAll()
    {
        List<UserCategory> userCategories = await _dbContext.UserCategories.ToListAsync();
        List<UserCategoryResponse> userCategoryResponses =
            userCategories.Select(uc => new UserCategoryResponse(uc)).ToList();

        return Ok(await _dbContext.UserCategories.ToListAsync());
    }
}