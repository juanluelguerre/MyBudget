using System;

namespace MyBudget.Api.Application.Customers.Queries
{
	public class CustomerAllViewModel
	{
		public int Id { get; private set; }
		public string FullName { get; private set; }
		public string BankAccount { get; private set; }

		public CustomerAllViewModel(int id, string fullName, string bankAccount)
		{
			Id = id;
			FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
			BankAccount = string.IsNullOrWhiteSpace(bankAccount) ? throw new ArgumentNullException(nameof(bankAccount)) : bankAccount;
		}
	}
}