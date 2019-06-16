using Dapper;
using MyBudget.Api.Application.Customers.Data;
using MyBudget.Api.Application.Customers.Domain.Aggregates;
using MyBudget.Api.Application.Customers.Domain.Interfaces;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Infrastructure
{
	public class DataReadonlyRepository<T> : IDataReadonlyService<T> where T : AggregateRoot
	{
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

		public async Task<T> FindOne(int id)
		{
			return await FindOne($"{GetQuery()} WHERE Id = @Id", id);
		}

		public async Task<IEnumerable<T>> FindAll()
		{
			return await FindAll(GetQuery(), null);
		}

		public async Task<T> FindOne(string query, int id)
		{
			using (var conn = Connection)
			{
				var result = await conn.QueryAsync<T>(query, new { id });
				return result.SingleOrDefault();
			}
		}

		public async Task<IEnumerable<T>> FindAll(string query, object parameters)
		{
			using (var conn = Connection)
			{
				var result = await conn.QueryAsync<T>(query, parameters);
				return result;
			}
		}

		private string GetQuery()
		{
			var t = typeof(T);
			var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);			

			var fields = props.Select(p => $"{p.Name}").ToList();

			var tableName = t.Name;
			switch (tableName)
			{
				//case nameof(Person):
				//	tableName = DataContext.TABLE_PERSON;
				//	break;
				case nameof(Customer):
					// TODO: Query/Table doesn't have some Fields
					fields.Remove(nameof(Customer.BankAccounts));
					break;
				case nameof(CustomerAccount):
					tableName = DataContext.TABLE_CUSTOMER_ACCOUNT;
					break;
				default:
					break;
			}

			return $"SELECT {string.Join(",", fields)} FROM {tableName}s";
		}
	}
}
