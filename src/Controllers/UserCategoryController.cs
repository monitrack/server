using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.UserCategory;
using server.Models;
using server.Responses.UserCategory;

namespace Server.Controllers;

public class UserCategoryController(ApplicationDbContext dbContext) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserCategoryResponse>> Create(CreateUserCategoryDto userCategoryDto)
    {
        UserCategory userCategory = new UserCategory
        {
            UserId = userCategoryDto.UserId,
            Name = userCategoryDto.Name,
        };

        dbContext.UserCategories.Add(userCategory);
        await dbContext.SaveChangesAsync();

        return Ok(new UserCategoryResponse(userCategory));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserCategoryResponse>> Update(int id, UpdateUserCategoryDto userCategoryDto)
    {
        UserCategory? userCategory = await dbContext.UserCategories.FirstOrDefaultAsync(u => u.Id == id);
        if (userCategory is null)
        {
            return NotFound($"User category with id {id} not found!");
        }

        userCategory.Name = userCategoryDto.Name;
        userCategory.UpdatedDate = DateTime.Now;

        await dbContext.SaveChangesAsync();

        return Ok(new UserCategoryResponse(userCategory));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        int rows = await dbContext.UserCategories.Where(u => u.Id == id).ExecuteDeleteAsync();
        if (rows == 0)
        {
            return NotFound($"User category with id {id} not found!");
        }

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserCategoryResponse>> GetById(int id)
    {
        UserCategory? userCategory = await dbContext.UserCategories.FirstOrDefaultAsync(u => u.Id == id);
        if (userCategory is null)
        {
            return NotFound($"User category with id {id} not found!");
        }

        return Ok(new UserCategoryResponse(userCategory));
    }

    [HttpGet]
    public async Task<ActionResult<List<UserCategoryResponse>>> GetAll()
    {
        List<UserCategory> userCategories = await dbContext.UserCategories.ToListAsync();
        List<UserCategoryResponse> userCategoryResponses =
            userCategories.Select(uc => new UserCategoryResponse(uc)).ToList();

        return Ok(await dbContext.UserCategories.ToListAsync());
    }
}