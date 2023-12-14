using FluentValidation;
using server.Dtos;

namespace server.Validators;

public class ExpenseDtoValidator : AbstractValidator<ExpenseDto>
{
    public ExpenseDtoValidator()
    {
        RuleFor(expense => expense.Amount)
            .NotEmpty();
        RuleFor(expense => expense.Date)
            .NotEmpty()
            .Must(BeAValidDate)
            .WithMessage("Please provide a valid date");
        RuleFor(expense => expense.Note)
            .MaximumLength(100)
            .WithMessage("Please provide a shorter note");
        RuleFor(expense => expense.Category)
            .NotEmpty();
        RuleFor(expense => expense.MethodId)
            .NotEmpty();
    }

    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default);
    }
}