using Microsoft.Extensions.Logging;
using MyBudget.Api.Infrastructure;
using System.Threading.Tasks;

namespace MyBudget.Api.Services
{
	public class BudgetService : IBudgetService
	{
		private readonly DataContext _context;
		private readonly ILogger<BudgetService> _logger;

		public BudgetService(DataContext contex, ILogger<BudgetService> logger)
		{
			_context = contex;
			_logger = logger;
		}

		public async Task<bool> Add(int value, string desc)
		{
			var budget = new Budget() { Amount = value, Description = desc };
			_context.Budget.Add(budget);

			return true;
		}

		public async Task<bool> Remove(int value, string desc)
		{
			var budget = new Budget() { Amount = (-1) * value, Description = desc };
			_context.Budget.Add(budget);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
