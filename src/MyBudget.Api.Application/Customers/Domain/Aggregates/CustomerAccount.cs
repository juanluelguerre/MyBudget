using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Application.Customers.Domain.Aggregates
{
	public class CustomerAccount : AggregateRoot
	{
		// [Required]
		[Key]
		public string BankAccount { get; private set; }		
		public bool MarkAsDefault { get; private set; }
		
		[Required]
		public int CustomerId {get; set;}
		// public Customer Customer { get; private set; }

		public CustomerAccount(int customerId, string bankAccount, bool markAsDefault=false)
		{
			if (customerId <= 0) throw new ArgumentException(nameof(customerId));
			CustomerId = customerId;
			BankAccount = string.IsNullOrWhiteSpace(bankAccount) ? throw new ArgumentNullException(nameof(bankAccount)) : bankAccount;
			MarkAsDefault = markAsDefault;
		}

		public static CustomerAccount CreateNew(int customerId, string bankAccount, bool markAsDefault)
		{
			return new CustomerAccount(customerId, bankAccount, markAsDefault);
		}
	}
}
