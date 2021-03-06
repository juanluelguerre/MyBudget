﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Domain.Interfaces
{
	public interface IDataReadonlyService<T>
	{
		Task<T> FindOne(int id);		
		Task<T> FindOne(string query, int id);
		Task<IEnumerable<T>> FindAll();
		Task<IEnumerable<T>> FindAll(string query, object parameters);
	}
}