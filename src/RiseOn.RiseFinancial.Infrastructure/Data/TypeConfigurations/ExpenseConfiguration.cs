using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiseOn.RiseFinancial.Core.ExpenseAggregate;
using RiseOn.RiseFinancial.Infrastructure.Data.Converters;
using DateOnlyConverter = System.ComponentModel.DateOnlyConverter;

namespace RiseOn.RiseFinancial.Infrastructure.Data.TypeConfigurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{

    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses",
            table =>
            {
                table.IsMemoryOptimized();
                table.IsTemporal(builder 
                    => builder.UseHistoryTable("ExpenseHistory"));
            });

        builder.Property(x => x.DueDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>();
    }
}