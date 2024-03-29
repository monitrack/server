using FluentValidation;

namespace server.Dtos.GeneralCategory;

public class UpdateGeneralCategoryDto
{
    public required string Name { get; set; }
}

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateGeneralCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty()
            .Length(3, 20)
            .WithMessage("Please provide a valid name with minimum length 3 and maximum 20");
    }
}