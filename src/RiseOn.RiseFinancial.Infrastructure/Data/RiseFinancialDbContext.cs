using Microsoft.EntityFrameworkCore;
using RiseOn.RiseFinancial.Core.ExpenseAggregate;

namespace RiseOn.RiseFinancial.Infrastructure.Data;

public class RiseFinancialDbContext : DbContext
{
    public RiseFinancialDbContext(DbContextOptions<RiseFinancialDbContext> options)
    : base(options)
    {}

    public DbSet<Expense> Expenses => Set<Expense>();
}