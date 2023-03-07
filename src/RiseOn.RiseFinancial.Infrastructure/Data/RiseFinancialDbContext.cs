using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RiseOn.RiseFinancial.Core.ExpenseAggregate;

namespace RiseOn.RiseFinancial.Infrastructure.Data;

public class RiseFinancialDbContext : DbContext
{
    public RiseFinancialDbContext(DbContextOptions<RiseFinancialDbContext> options)
        : base(options) { }

    public DbSet<Expense> Expenses
        => Set<Expense>();

    public DbSet<Category> Categories
        => Set<Category>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<string>()
            .HaveMaxLength(150);

        configurationBuilder
            .Properties<decimal>()
            .HavePrecision(15, 2);

        configurationBuilder
            .Properties<Enum>()
            .HaveConversion<string>()
            .HaveMaxLength(50);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}