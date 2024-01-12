using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Context;
using server.Dtos.Category;
using server.Models;
using server.Validators;

namespace Server.Controllers;

public class CategoryController : ApiControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Create(CreateCategoryDto createCategoryDto)
    {
        DateTime now = DateTime.Now;
        Category category = new Category
        {
            Name = createCategoryDto.Name,
            CreatedDate = now,
            UpdatedDate = now
        };

        CategoryValidator validator = new CategoryValidator(_dbContext);
        ValidationResult validationResult = await validator.ValidateAsync(category);
        if (!validationResult.IsValid)
        {
            return ValidationProblem(validationResult.ToString());
        }

        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> Update(int id, UpdateCategoryDto updateCategoryDto)
    {
        Category? category = await _dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        category.Name = updateCategoryDto.Name;
        category.UpdatedDate = DateTime.Now;

        CategoryValidator validator = new CategoryValidator(_dbContext);
        ValidationResult validationResult = await validator.ValidateAsync(category);
        if (!validationResult.IsValid)
        {
            return ValidationProblem(validationResult.ToString());
        }

        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();

        return Ok(category);
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
    public async Task<ActionResult<Category>> GetById(int id)
    {
        Category? category = await _dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound($"Category with id {id} not found!");
        }

        return Ok(category);
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAll()
    {
        return Ok(await _dbContext.Categories.ToListAsync());
    }
}