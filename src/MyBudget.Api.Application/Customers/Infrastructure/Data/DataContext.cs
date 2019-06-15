using Microsoft.EntityFrameworkCore;
using MyBudget.Api.Application.Customers.Domain.Aggregates;

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
			modelBuilder.Entity<CustomerAccount>().ToTable("CustomerAccounts");
			modelBuilder.Entity<Budget>().ToTable("Budgets");


			// modelBuilder.Entity<CustomerAccount>().HasKey(c => new { c.Id, c.BankAccount });
			// modelBuilder.Entity<Customer>().HasMany(c => c.BankAccounts);

			//modelBuilder.Entity<Customer>()
			//.HasOne(p => p.)
			//.WithMany(b => b);
		}
	}
}
