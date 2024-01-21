using FluentValidation;

namespace server.Dtos.UserCategory;

public class CreateUserCategoryDto
{
    public required int UserId { get; set; }
    public required string Name { get; set; }
}

public class CreateUserCategoryDtoValidator : AbstractValidator<CreateUserCategoryDto>
{
    public CreateUserCategoryDtoValidator()
    {
        RuleFor(category => category.UserId)
            .NotEmpty()
            .WithMessage("User id is required!");
        
        RuleFor(category => category.Name)
            .NotEmpty()
            .WithMessage("Name is required!")
            .Length(3, 20)
            .WithMessage("Please provide a valid category name with minimum length 3 and maximum 20");
    }
}