using FluentValidation;

namespace server.Dtos.GeneralCategory;

public class CreateGeneralCategoryDto
{
    public required string Name { get; set; }
}

public class CreateCategoryDtoValidator : AbstractValidator<CreateGeneralCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty()
            .Length(3, 20)
            .WithMessage("Please provide a valid name with minimum length 3 and maximum 20");
    }
}