using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudget.Finances.Api.Application.Domain.Interfaces
{
	public interface IDataReadonlyRepository
	{
		Task<CustomerViewModel> FindOne(int id);				
		Task<IEnumerable<CustomerViewModel>> FindAll();		
	}
}