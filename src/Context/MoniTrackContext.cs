using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Context;

public class MoniTrackContext : DbContext
{
    public DbSet<Income> Incomes { get; set; } = null!;
    public DbSet<Expense> Expenses { get; set; } = null!;
    public DbSet<Method> Method { get; set; } = null!;
    
    public string DbPath { get; }
   
    // todo refactor context
    // Ифтихор чи қавру балое кардай ки ҳичка намефаҳма и чхе кор мекна...
    public MoniTrackContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}