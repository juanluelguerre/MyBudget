using Microsoft.EntityFrameworkCore;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Data;

namespace MyBudget.Api.Application.Customers.Infrastructure
{
	public class DataRepository<T> : IDataRepository<T> where T: AggregateRoot
	{
		private DataContext _context;

		public DataRepository(DataContext context)
		{
			_context = context;
		}

		public bool Delete(int id)
		{
			T t = _context.Find<T>(id);
			var result = _context.Remove<T>(t);
			return (result.State == EntityState.Deleted);
		}

		public bool Add(T entity)
		{
			_context.Add<T>(entity);
			var result = _context.SaveChanges();
			return result > 0;
		}

		public bool Update(T entity)
		{
			_context.Update<T>(entity);
			var result = _context.SaveChanges();
			return result > 0;
		}
	}
}
