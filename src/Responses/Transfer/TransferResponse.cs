namespace server.Responses.Transfer;

public class TransferResponse
{
    public required int AccountFromId { get; set; }
    public required int AccountToId { get; set; }
    public string? Description { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now;
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }
}