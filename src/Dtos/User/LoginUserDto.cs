using FluentValidation;

namespace server.Dtos.User;

public class LoginUserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
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