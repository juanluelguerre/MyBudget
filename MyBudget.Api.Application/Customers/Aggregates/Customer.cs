using System;

namespace MyBudget.Api.Application.Customers.Aggregates
{
	public class Customer : Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? CustomerFrom { get; set; }
		public string BankAccount { get; set; }
		public bool Active { get; private set; }

		public Customer(int id, string firstName, string lastName, DateTime? customerFrom, string bankAccount)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			BankAccount = bankAccount;
			CustomerFrom = customerFrom;
		}

		public static Customer CreateNew(int id, string firstName, string lastName, DateTime? customerFrom, string bankAccount)
		{
			return new Customer(id, firstName, lastName, customerFrom, bankAccount);
		}
	}
}
