using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Context;

public class ApplicationDbContext : DbContext
{
    #nullable disable
    public DbSet<User> Users { get; set; }
    public DbSet<GeneralCategory> GeneralCategories { get; set; }
    public DbSet<UserCategory> UserCategories { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}