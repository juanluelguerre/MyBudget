using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBudget.Api.Infrastructure.EntityConfiguration
{
	internal class BudgetEntityTypeConfiguration
		: IEntityTypeConfiguration<Budget>
	{
		public void Configure(EntityTypeBuilder<Budget> builder)
		{
			builder.ToTable("Budget");

			builder.HasKey(ci => ci.Id);

			builder.Property(ci => ci.Id)			   
			   .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
			   .IsRequired();

			builder.Property(cb => cb.Amount)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(cb => cb.Description)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}
