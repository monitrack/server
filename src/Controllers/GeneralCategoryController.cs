using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.GeneralCategory;
using server.Models;
using server.Responses.GeneralCategory;

namespace Server.Controllers;

public class GeneralCategoryController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public GeneralCategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<GeneralCategoryResponse>> Create(CreateGeneralCategoryDto createGeneralCategoryDto)
    {
        GeneralCategory generalCategory = new() {
            Name = createGeneralCategoryDto.Name,
        };

        _dbContext.GeneralCategories.Add(generalCategory);
        await _dbContext.SaveChangesAsync();

        return Ok(new GeneralCategoryResponse(generalCategory));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GeneralCategoryResponse>> Update(int id, UpdateGeneralCategoryDto updateGeneralCategoryDto)
    {
        GeneralCategory? category = await _dbContext.GeneralCategories.FirstOrDefaultAsync(g => g.Id == id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        category.Name = updateGeneralCategoryDto.Name;
        category.UpdatedDate = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return Ok(new GeneralCategoryResponse(category));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        int rows = await _dbContext.GeneralCategories.Where(g => g.Id == id).ExecuteDeleteAsync();
        if (rows == 0)
        {
            return NotFound($"Category with id {id} not found!");
        }

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GeneralCategoryResponse>> GetById(int id)
    {
        GeneralCategory? category = await _dbContext.GeneralCategories.FirstOrDefaultAsync(g => g.Id == id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        return Ok(new GeneralCategoryResponse(category));
    }

    [HttpGet]
    public async Task<ActionResult<List<GeneralCategoryResponse>>> GetAll()
    {
        List<GeneralCategory> categories = await _dbContext.GeneralCategories.ToListAsync();
        List<GeneralCategoryResponse> responses = categories.Select(category => new GeneralCategoryResponse(category)).ToList();

        return Ok(responses);
    }
}