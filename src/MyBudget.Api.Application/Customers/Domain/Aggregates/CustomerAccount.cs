using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Application.Customers.Domain.Aggregates
{
	public class CustomerAccount : AggregateRoot
	{
		[Required]
		public string BankAccount { get; set; }

		public bool MarkAsDefault { get; private set; }
		

		public CustomerAccount(int id, string bankAccount, bool markAsDefault=false)
		{
			if (id <= 0) throw new ArgumentException(nameof(id));
			Id = id;
			BankAccount = string.IsNullOrWhiteSpace(bankAccount) ? throw new ArgumentNullException(nameof(bankAccount)) : bankAccount;
			MarkAsDefault = markAsDefault;
		}

		public static CustomerAccount CreateNew(int id, string bankAccount, bool markAsDefault)
		{
			return new CustomerAccount(id, bankAccount, markAsDefault);
		}
	}
}
