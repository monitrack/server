namespace server.Models;

public class Income
{
    public float Amount { get; set; }

    public required string Date { get; set; }
    
    public string? Comment { get; set; }

    public required string Method { get; set; }
}