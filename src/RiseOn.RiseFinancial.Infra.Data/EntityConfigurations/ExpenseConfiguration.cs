using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiseOn.RiseFinancial.Core.ExpenseAggregate;

namespace RiseOn.RiseFinancial.Infra.Data.EntityConfigurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses",
            table =>
            {
                table.IsTemporal(tableBuilder 
                    => tableBuilder.UseHistoryTable("ExpenseHistory"));
            });

        builder.HasKey(x => x.Id);
    }
}