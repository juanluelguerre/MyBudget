using Microsoft.EntityFrameworkCore;
using MyBudget.Api.Application.Customers.Aggregates;

namespace MyBudget.Api.Application.Customers.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		//public DbSet<PersonEntity> People { get; set; }
		//public DbSet<CustomerEntity> Customers { get; set; }
		//public DbSet<BudgetEntity> Budget { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>().ToTable("People");
			modelBuilder.Entity<Customer>().ToTable("Customers");
			modelBuilder.Entity<Budget>().ToTable("Budgets");

			//modelBuilder.Entity<Bank>().HasKey(b => new { b.Id, b.CustomerId });

		}
	}
}
