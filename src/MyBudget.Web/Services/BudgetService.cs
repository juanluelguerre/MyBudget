using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBudget.Api.Services
{
	public class BudgetService : IBudgetService
	{
		private readonly ILogger<BudgetService> _logger;
		private readonly HttpClient _httpClient;

		public BudgetService(HttpClient httpClient, ILogger<BudgetService> logger)
		{
			_httpClient = httpClient;
			_logger = logger;
		}

		public async Task<bool> Add(int value, string desc)
		{
			// _httpClient.

			return true;
		}

		public async Task<bool> Remove(int value, string desc)
		{
			//_httpClient.

			return true;
		}
	}
}
