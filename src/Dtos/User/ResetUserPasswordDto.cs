namespace server.Dtos.User;

public class ResetUserPasswordDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordResetToken { get; set; }
}