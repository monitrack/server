using server.Models;

namespace server.Responses.Income;

public class IncomeResponse
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string? Note { get; set; }

    public int CategoryId { get; set; }

    public string CategoryType { get; set; }

    public int AccountId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public IncomeResponse(Models.Income income)
    {
        Id = income.Id;
        Amount = income.Amount;
        Date = income.Date;
        Note = income.Note;
        CategoryId = income.CategoryId;
        CategoryType = income.CategoryType;
        AccountId = income.AccountId;
        CreatedDate = income.CreatedDate;
        UpdatedDate = income.UpdatedDate;
    }
}