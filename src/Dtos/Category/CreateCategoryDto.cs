using FluentValidation;

namespace server.Dtos.Category;

public class CreateCategoryDto
{
    public required string Name { get; set; }
}

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty()
            .Length(3, 20)
            .WithMessage("Please provide a valid name with minimum length 3 and maximum 20");
    }
}