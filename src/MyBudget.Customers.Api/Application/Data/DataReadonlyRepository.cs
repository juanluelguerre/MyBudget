using Dapper;
using MyBudget.Customers.Api.Application.Domain.Interfaces;
using MyBudget.Customers.Api.Application.Queries;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyBudget.Customers.Api.Application.Data
{
	public class DataReadonlyRepository : IDataReadonlyRepository
	{
		private const string BASE_QUERY = 
			@"SELECT c.Id, c.FirstName, c.LastName, ca.BankAccount, ca.MarkAsDefault
			FROM MyBudget.Customers c
			LEFT JOIN MyBudget.CustomersAccounts ca
			ON c.Id = ca.CustomerId
			WHERE ca.BankAccount IS NOT NULL
			AND c.Active";

		private readonly string _connectionString;
		
		public DataReadonlyRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IDbConnection Connection
		{
			get
			{
				return new MySqlConnection(_connectionString);
			}
		}

		public async Task<CustomerViewModel> FindOne(int id)
		{			
			using (var conn = Connection)
			{
				var result = await conn.QueryFirstOrDefaultAsync<CustomerViewModel>($"{BASE_QUERY} AND c.Id = @Id;", id);
				return result;
			}
		}

		public async Task<IEnumerable<CustomerViewModel>> FindAll()
		{
			using (var conn = Connection)
			{				
				var result = await conn.QueryAsync<CustomerViewModel>($"{BASE_QUERY};");
				return result;
			}
		}		
	}
}
