using MyBudget.Api.Application.Customers.Aggregates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Infrastructure.Mocks
{
	public class DataReadonlyRepositoryCustomerMock : IDataReadonlyRepository<Customer>
	{
		private readonly string _connectionString;

		public DataReadonlyRepositoryCustomerMock(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<IEnumerable<Customer>> FindAll()
		{
			var list = new List<Customer> { GetOne(1) };
			return await Task.FromResult(list);
		}

		public async Task<IEnumerable<Customer>> FindAll(string query, object parameters)
		{
			var list = new List<Customer> { GetOne(1) };
			return await Task.FromResult(list);
		}

		public async Task<Customer> FindOne(int id)
		{
			return await Task.FromResult(GetOne(id));
		}

		public async Task<Customer> FindOne(string query, int id)
		{
			return await Task.FromResult(GetOne(id));
		}

		private Customer GetOne(int id)
		{
			return new Customer(id, "Juan Luis", "Guerrero Minero", DateTime.Now, Guid.NewGuid().ToString("N"));
		}
	}
}
