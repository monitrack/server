using FluentValidation;

namespace server.Dtos.Account;

public class CreateAccountDto
{
    public required string Name { get; set; } 

    public required decimal Balance { get; set; } 

    public required string Currency { get; set; }

    public string? Description { get; set; } = null;

    public required int UserId { get; set; }
}

public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
{
    public CreateAccountDtoValidator()
    {
        RuleFor(account => account.Name)
            .NotEmpty()
            .WithMessage("Name is required!");
        RuleFor(account => account.Balance)
            .NotEmpty()
            .WithMessage("Balance is required!");
        RuleFor(account => account.Currency)
            .NotEmpty()
            .WithMessage("Currency is required!")
            .Length(3)
            .WithMessage("Please provide a valid currency");
        RuleFor(account => account.Description)
            .MaximumLength(255)
            .WithMessage("Please provide a shorter description!");
        RuleFor(account => account.UserId)
            .NotEmpty()
            .WithMessage("User id is required!");
    }
}