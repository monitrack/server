using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.GeneralCategory;
using server.Models;
using server.Responses.GeneralCategory;

namespace Server.Controllers;

public class GeneralCategoryController(ApplicationDbContext dbContext) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GeneralCategoryResponse>> Create(CreateGeneralCategoryDto createGeneralCategoryDto)
    {
        GeneralCategory generalCategory = new() {
            Name = createGeneralCategoryDto.Name,
        };

        dbContext.GeneralCategories.Add(generalCategory);
        await dbContext.SaveChangesAsync();

        return Ok(new GeneralCategoryResponse(generalCategory));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GeneralCategoryResponse>> Update(int id, UpdateGeneralCategoryDto updateGeneralCategoryDto)
    {
        GeneralCategory? category = await dbContext.GeneralCategories.FirstOrDefaultAsync(g => g.Id == id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        category.Name = updateGeneralCategoryDto.Name;
        category.UpdatedDate = DateTime.Now;

        await dbContext.SaveChangesAsync();

        return Ok(new GeneralCategoryResponse(category));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        int rows = await dbContext.GeneralCategories.Where(g => g.Id == id).ExecuteDeleteAsync();
        if (rows == 0)
        {
            return NotFound($"Category with id {id} not found!");
        }

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GeneralCategoryResponse>> GetById(int id)
    {
        GeneralCategory? category = await dbContext.GeneralCategories.FirstOrDefaultAsync(g => g.Id == id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        return Ok(new GeneralCategoryResponse(category));
    }

    [HttpGet]
    public async Task<ActionResult<List<GeneralCategoryResponse>>> GetAll()
    {
        List<GeneralCategory> categories = await dbContext.GeneralCategories.ToListAsync();
        List<GeneralCategoryResponse> responses = categories.Select(category => new GeneralCategoryResponse(category)).ToList();

        return Ok(responses);
    }
}