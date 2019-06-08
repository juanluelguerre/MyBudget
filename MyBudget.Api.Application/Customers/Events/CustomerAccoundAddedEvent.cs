using System;
using MediatR;

namespace MyBudget.Api.Application.Customers.Events
{
	public class CustomerAccountAddedEvent : INotification
	{
		public int Id { get; private set; }
		public string BankAccount { get; private set; }

		public CustomerAccountAddedEvent(int id, string bankAccount)
		{
			if (id <= 0) throw new ArgumentException(nameof(id));
			Id = id;
			BankAccount = bankAccount ?? throw new ArgumentNullException(nameof(bankAccount));
		}
	}
}
