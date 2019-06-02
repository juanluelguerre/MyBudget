using System;

namespace MyBudget.Api.Application.Customers.Queries
{
	public class CustomerAllQueryResult
	{
		public int Id { get; private set; }
		public string FullName { get; private set; }
		public string BankAccount { get; private set; }

		public CustomerAllQueryResult(int id, string fullName, string bankAccount)
		{
			Id = id;
			FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
			BankAccount = bankAccount ?? throw new ArgumentNullException(nameof(bankAccount));
		}
	}
}