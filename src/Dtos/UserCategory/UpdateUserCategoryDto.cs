using FluentValidation;

namespace server.Dtos.UserCategory;

public class UpdateUserCategoryDto
{
    public required string Name { get; set; }
}

public class UpdateUserCategoryDtoValidator : AbstractValidator<UpdateUserCategoryDto>
{
    public UpdateUserCategoryDtoValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty()
            .WithMessage("Name is required!")
            .Length(3, 20)
            .WithMessage("Please provide a valid category name with minimum length 3 and maximum 20");
    }
}