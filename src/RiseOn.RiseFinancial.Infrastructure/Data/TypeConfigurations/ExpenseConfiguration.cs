using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiseOn.RiseFinancial.Core.ExpenseAggregate;
using RiseOn.RiseFinancial.Infrastructure.Data.Converters;

namespace RiseOn.RiseFinancial.Infrastructure.Data.TypeConfigurations;

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

        builder.Property(x => x.DueDate)
            .HasConversion<DateConverter, DateComparer>();
    }
}