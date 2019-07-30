using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBudget.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBudget.Api.Services
{
	public class BudgetService : IBudgetService
	{
		private readonly ILogger<BudgetService> _logger;
		private readonly HttpClient _httpClient;
		private readonly IOptions<AppSettings> _settings;
		private readonly string _apiUrl;

		public BudgetService(HttpClient httpClient, IOptions<AppSettings> settings, ILogger<BudgetService> logger)
		{
			_httpClient = httpClient;
			_settings = settings;
			_logger = logger;

			_apiUrl = $"{_settings.Value.ApiUrl}/api/v1/";
		}

		public async Task<bool> Add(double value, string desc)
		{
			var uri = $"{_apiUrl}/Add";

			var budget = new Budget() { Amount = value, Description = desc };
			var budgetContent = new StringContent(JsonConvert.SerializeObject(budget), System.Text.Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(uri, budgetContent);
			response.EnsureSuccessStatusCode();

			return true;
		}

		public async Task<bool> Remove(double value, string desc)
		{
			var uri = $"{_apiUrl}/Remove";

			var budget = new Budget() { Amount = value, Description = desc };
			var budgetContent = new StringContent(JsonConvert.SerializeObject(budget), System.Text.Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(uri, budgetContent);
			response.EnsureSuccessStatusCode();

			return true;
		}
	}
}
