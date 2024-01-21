using FluentValidation;

namespace server.Dtos.Income;

public class CreateIncomeDto
{
    public required decimal Amount { get; set; }

    public required DateTime Date { get; set; }
    
    public string? Note { get; set; }

    public required int CategoryId { get; set; }

    public required string CategoryType { get; set; }

    public required int MethodId { get; set; }
}

public class CreateIncomeDtoValidator : AbstractValidator<CreateIncomeDto>
{
    public CreateIncomeDtoValidator()
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
        
        RuleFor(income => income.MethodId)
            .NotEmpty()
            .WithMessage("Method id is required!");
    }
}