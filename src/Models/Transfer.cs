namespace server.Models;

public class Transfer : BaseEntity
{
    public required decimal Amount { get; set; }
    public required int AccountFromId { get; set; }
    public required int AccountToId { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now;
    public string? Description { get; set; }
}