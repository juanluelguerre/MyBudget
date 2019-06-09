using System;
using System.Collections.Generic;

namespace MyBudget.Api.Application.Customers.Domain.Aggregates
{
	public class Customer : Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? CustomerFrom { get; set; }
		public ICollection<CustomerAccount> BankAccounts { get; set; }
		public string BankAccount { get; set; }
		public bool Active { get; private set; }

		public Customer(int id, string firstName, string lastName, DateTime? customerFrom, string bankAccount)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			CustomerFrom = customerFrom;
			BankAccounts = new List<CustomerAccount>();
			if (!string.IsNullOrWhiteSpace(bankAccount))
			{
				BankAccounts.Add(new CustomerAccount(id, bankAccount, true));
			}			
		}

		public static Customer CreateNew(int id, string firstName, string lastName, DateTime? customerFrom, string bankAccount)
		{
			return new Customer(id, firstName, lastName, customerFrom, bankAccount);
		}

		public static Customer CreateNew(int id, string firstName, string lastName, DateTime? customerFrom)
		{
			return new Customer(id, firstName, lastName, customerFrom, null);
		}
	}
}
