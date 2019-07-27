using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Customers.Api.Application.Customers.Commands
{
	public class CustomerAccountAddCommand : IRequest<bool>
	{
		[Required]
		public int Id { get; private set; }
		[Required]
		public string BankAccount{ get; private set; }
		public bool MarkAsDefault { get; private set; }

		public CustomerAccountAddCommand(int id, string bankAccount, bool markAsDefefault)
		{
			if (id <= 0) throw new ArgumentException(nameof(id));
			Id = id;
			BankAccount = string.IsNullOrWhiteSpace(bankAccount) ? throw new ArgumentNullException(nameof(bankAccount)) : bankAccount;
			MarkAsDefault = markAsDefefault;
		}
	}
}
