using Microsoft.EntityFrameworkCore;
using MyBudget.Finances.Api.Application.Domain.Aggregates;
using System;

namespace MyBudget.Customers.Api.Application.Data
{
	public class DataContext : DbContext
	{		
		public const string TABLE_ACCOUNTS = "Accounts";
		public const string TABLE_OPERATIONS = "Products";	

		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{			
			modelBuilder.Entity<Account>().ToTable(TABLE_ACCOUNTS);
			modelBuilder.Entity<Operation>().ToTable(TABLE_OPERATIONS);

			// Bug: MySQL buhttps://stackoverflow.com/questions/47330796/entity-framework-core-with-mysql-unknown-column-in-field-list
			//		Also not suportend ICollection<> properties using "Pomelo..." Nuget.
			// modelBuilder.Entity<Customer>().Property(a => a.BankAccounts).HasColumnName("BankAccount");


			// Master data
			modelBuilder.Entity<Operation>().HasData(
				new Operation(1, "Deposit"),
				new Operation(2, "Transfer")
			);

			// Master data
			modelBuilder.Entity<Product>().HasData(
				new Product(1, "Saving Account", "Standard deposit account kept that pay interest"),
				new Product(2, "Loan", "Is a type of debt. The borrower needs to repay the lender the sum of money loaned part by part over time in order to clear the debt"),
				new Product(3, "mortgage", "Is a way to use one's real property as a guarantee for a loan to get money. Real property can be land, a house, or a building")
			);


			// Sample data
			modelBuilder.Entity<Account>().HasData(
				new Account("ES12", "1234", "12", "12345678", 100.00),
				new Account("ES12", "1234", "12", "87654321", 200.00)
			);
		}
	}
}
