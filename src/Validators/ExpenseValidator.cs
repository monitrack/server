using FluentValidation;
using server.Models;

namespace server.Validators;

public class ExpenseValidator : AbstractValidator<Expense>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Amount)
            .NotEmpty()
            .WithMessage("Amount is required!");
        RuleFor(expense => expense.Date)
            .NotEmpty()
            .WithMessage("Date is required!");
        RuleFor(expense => expense.Note)
            .MaximumLength(100)
            .WithMessage("Please provide a shorter note");
        RuleFor(expense => expense.CategoryId)
            .NotEmpty()
            .WithMessage("Category id is required!");
        RuleFor(expense => expense.MethodId)
            .NotEmpty()
            .WithMessage("Method id is required!");
    }
}