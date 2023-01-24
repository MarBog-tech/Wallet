using Microsoft.EntityFrameworkCore;
using Wallet.DAL.Configuration;
using Wallet.Domain.Entity;

namespace Wallet.DAL;

public class AppDbContext : DbContext
{
    
    public DbSet<User>? Users { get; set; }
    public DbSet<UserCard> UserCards { get; set; }  = null!;
    public DbSet<Profit> Profits { get; set; }  = null!;
    public DbSet<Expenses> Expenses { get; set; }  = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserCardConfiguration());
        modelBuilder.ApplyConfiguration(new ProfitConfiguration());
        modelBuilder.ApplyConfiguration(new ExpensesConfiguration());
    }

}