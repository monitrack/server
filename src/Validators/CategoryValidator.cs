using FluentValidation;
using server.Context;
using server.Dtos;
using server.Models;

namespace server.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(category => category.Name)
            .NotEmpty()
            .Length(3, 20)
            .WithMessage("Please provide a valid name with minimum length 3 and maximum 20")
            .Must(BeAUniqueName)
            .WithMessage("Category with such name already exists");
    }

    private bool BeAUniqueName(Category category, string name)
    {
        Category? categoryWithSuchNameInDb = _dbContext.Categories.SingleOrDefault(c => c.Name == name);

        if (categoryWithSuchNameInDb == null)
        {
            return true;
        }

        return categoryWithSuchNameInDb.Id == category.Id;
    }
}