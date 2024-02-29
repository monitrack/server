namespace server.Responses.Account;

public class AccountResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Balance { get; set; }

    public string Currency { get; set; }

    public string? Description { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public AccountResponse(Models.Account account)
    {
        Id = account.Id;
        Name = account.Name;
        Balance = account.Balance;
        Currency = account.Currency;
        Description = account.Description;
        UserId = account.UserId;
        CreatedDate = account.CreatedDate;
        UpdatedDate = account.UpdatedDate;
    }
}