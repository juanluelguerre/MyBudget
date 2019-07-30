using System.Threading.Tasks;

namespace MyBudget.Api.Services
{
	public interface IBudgetService
	{
		Task<bool> Add(double value, string desc);
		Task<bool> Remove(double value, string desc);
	}
}
