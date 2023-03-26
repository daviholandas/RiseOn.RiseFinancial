using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiseOn.RiseFinancial.Core.ExpenseAggregate;

namespace RiseOn.RiseFinancial.Infra.Data.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", options =>
        {
            options.IsTemporal();
        });

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany(x => x.Expenses)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);
    }
}