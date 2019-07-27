using Microsoft.EntityFrameworkCore;

namespace MyBudget.Api.Infrastructure
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Budget> Budget { get; set; } 
	}
}
