using MediatR;
using System;

namespace MyBudget.Api.Application.Customers.Commands
{
	public class BudgetAddCommand : IRequest<bool>
	{
		public string BankId { get; private set; }
		public string AccountId { get; private set; }
		public double Amount { get; private set; }

		public BudgetAddCommand(string bankId, string accountId, double amount)
		{
			BankId = bankId ?? throw new ArgumentNullException(nameof(bankId));
			AccountId = accountId ?? throw new ArgumentNullException(nameof(accountId));
			Amount = amount;
		}
	}
}
