using FluentValidation;

namespace server.Dtos.Transfer;

public class UpdateTransferDto
{
    public required decimal Amount { get; set; }
    public required int AccountFromId { get; set; }
    public required int AccountToId { get; set; }
    public required DateTime Date { get; set; }
    public string? Description { get; set; }
}

public class UpdateTransferDtoValidator : AbstractValidator<UpdateTransferDto>
{
    public UpdateTransferDtoValidator()
    {
        RuleFor(transfer => transfer.Amount)
            .NotNull()
            .WithMessage("Amount is required!")
            .GreaterThan(0)
            .WithMessage("Amount should be greater than 0!");
        RuleFor(transfer => transfer.AccountFromId)
            .NotEmpty()
            .WithMessage("Account from id is required!");
        RuleFor(transfer => transfer.AccountToId)
            .NotEmpty()
            .WithMessage("Account to id is required!");
        RuleFor(transfer => transfer.Date)
            .NotEmpty()
            .WithMessage("Date is required!");
        RuleFor(transfer => transfer.Description)
            .MaximumLength(100)
            .WithMessage("Please provide a shorter description");
    }
}