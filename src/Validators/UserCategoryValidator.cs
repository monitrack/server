using FluentValidation;
using server.Context;
using server.Models;

namespace server.Validators;

public class UserCategoryValidator : AbstractValidator<UserCategory>
{
    private readonly ApplicationDbContext _dbContext;

    public UserCategoryValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(category => category.Name)
            .NotEmpty()
            .Length(3, 20)
            .WithMessage("Please provide a valid name with minimum length 3 and maximum 20")
            .Must(BeAUniqueName)
            .WithMessage("Category with such name already exists");
    }

    private bool BeAUniqueName(UserCategory category, string name)
    {
        UserCategory? categoryWithSuchNameInDb = _dbContext.UserCategories.SingleOrDefault(c => c.Name == name);

        if (categoryWithSuchNameInDb == null)
        {
            return true;
        }

        return categoryWithSuchNameInDb.Id == category.Id;
    }
}