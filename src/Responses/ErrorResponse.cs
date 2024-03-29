namespace server.Responses;

public class ErrorResponse
{
    public required string Error { get; set; }
    public required int Status { get; set; }
}