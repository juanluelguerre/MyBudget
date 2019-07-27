using MyBudget.Customers.Api.Application.Domain.Interfaces;
using MyBudget.Customers.Api.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudget.Customers.Api.Application.Data.Mocks
{
	public class DataReadonlyRepositoryCustomerMock : IDataReadonlyRepository
	{
		public async Task<IEnumerable<CustomerViewModel>> FindAll()
		{
			var list = new List<CustomerViewModel> { GetOne(1) };
			return await Task.FromResult(list);
		}
	
		public async Task<CustomerViewModel> FindOne(int id)
		{
			return await Task.FromResult(GetOne(id));
		}

		private CustomerViewModel GetOne(int id)
		{			
			return new CustomerViewModel(id, "Juan Luis", "Guerrero Minero",  Guid.NewGuid().ToString("N"), true);
		}
	}
}
