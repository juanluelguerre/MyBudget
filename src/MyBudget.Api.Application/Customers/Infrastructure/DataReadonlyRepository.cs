using Dapper;
using MyBudget.Api.Application.Customers.Domain.Aggregates;
using MyBudget.Api.Application.Customers.Domain.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
				return new SqlConnection(_connectionString);
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
			
			return $"SELECT {string.Join(",", fields)} FROM {t.Name}";
		}
	}
}
