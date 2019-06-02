namespace MyBudget.Api.Application.Customers.Infrastructure
{
	public interface IDataRepository<T>
	{
		bool Add(T entity);
		bool Update(T entity);
		bool Delete(int id);
	}
}