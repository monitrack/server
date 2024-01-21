using FluentValidation;

namespace server.Dtos.Expense;

public class UpdateExpenseDto
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }

    public string? Note { get; set; } = null;

    public required int CategoryId { get; set; }

    public required string CategoryType { get; set; }

    public required int MethodId { get; set; }
}

public class UpdateExpenseDtoValidator : AbstractValidator<UpdateExpenseDto>
{
    public UpdateExpenseDtoValidator()
    {
        RuleFor(expense => expense.Amount)
            .NotEmpty()
            .WithMessage("Amount is required!")
            .GreaterThan(0)
            .WithMessage("Amount should be greater than 0!");
        RuleFor(expense => expense.Date)
            .NotEmpty()
            .WithMessage("Date is required!");
        RuleFor(expense => expense.Note)
            .MaximumLength(100)
            .WithMessage("Please provide a shorter note");
        RuleFor(expense => expense.CategoryId)
            .NotEmpty()
            .WithMessage("Category id is required!");
        RuleFor(expense => expense.CategoryType)
            .NotEmpty()
            .WithMessage("Category type is required!");
        RuleFor(expense => expense.MethodId)
            .NotEmpty()
            .WithMessage("Method id is required!");
    }
}