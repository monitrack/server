using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Category;
using server.Models;
using server.Responses.Category;

namespace Server.Controllers;

public class CategoryController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> Create(CreateCategoryDto createCategoryDto)
    {
        DateTime now = DateTime.Now;
        Category category = new Category
        {
            Name = createCategoryDto.Name,
            CreatedDate = now,
            UpdatedDate = now
        };

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();

        return Ok(new CategoryResponse(category));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryResponse>> Update(int id, UpdateCategoryDto updateCategoryDto)
    {
        Category? category = await _dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        category.Name = updateCategoryDto.Name;
        category.UpdatedDate = DateTime.Now;

        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();

        return Ok(new CategoryResponse(category));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Category? category = await _dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponse>> GetById(int id)
    {
        Category? category = await _dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        return Ok(new CategoryResponse(category));
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll()
    {
        List<Category> categories = await _dbContext.Categories.ToListAsync();
        List<CategoryResponse> responses = categories.Select(category => new CategoryResponse(category)).ToList();

        return Ok(responses);
    }
}