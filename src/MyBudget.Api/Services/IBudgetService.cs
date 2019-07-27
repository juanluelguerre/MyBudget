using System.Threading.Tasks;

namespace MyBudget.Api.Services
{
	public interface IBudgetService
	{
		Task<bool> Add(int value, string desc);
		Task<bool> Remove(int value, string desc);
	}
}
