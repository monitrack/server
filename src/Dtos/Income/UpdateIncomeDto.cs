using FluentValidation;

namespace server.Dtos.Income;

public class UpdateIncomeDto
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }

    public string? Note { get; set; }

    public required int CategoryId { get; set; }

    public required string CategoryType { get; set; }

    public required int AccountId { get; set; }
}

public class UpdateIncomeDtoValidator : AbstractValidator<UpdateIncomeDto>
{
    public UpdateIncomeDtoValidator()
    {
        RuleFor(income => income.Amount)
            .NotEmpty()
            .WithMessage("Amount is required!")
            .GreaterThan(0)
            .WithMessage("Amount should be greater than 0!");

        RuleFor(income => income.Date)
            .NotEmpty()
            .WithMessage("Date is required!");

        RuleFor(income => income.Note)
            .MaximumLength(100)
            .WithMessage("Please provide a shorter note");

        RuleFor(income => income.CategoryId)
            .NotEmpty()
            .WithMessage("Category id is required!");

        RuleFor(income => income.CategoryType)
            .NotEmpty()
            .WithMessage("Category type is required!");

        RuleFor(income => income.AccountId)
            .NotEmpty()
            .WithMessage("Account id is required!");
    }
}