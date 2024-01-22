using FluentValidation;

namespace server.Dtos.User;

public class CreateUserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("Name is required!");

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is required!")
            .EmailAddress()
            .WithMessage("Email is not valid!");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Email is required!");
    }
}