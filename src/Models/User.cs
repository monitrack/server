namespace server.Models;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime? EmailConfirmedDate { get; set; }
    public string EmailConfirmationToken { get; set; } = null!;
    public DateTime? EmailConfirmationTokenExpirationDate { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpirationDate { get; set; }
    public virtual List<Account> Accounts { get; set; } = null!;
}