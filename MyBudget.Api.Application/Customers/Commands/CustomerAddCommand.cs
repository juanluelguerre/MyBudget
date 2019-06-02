using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Application.Customers.Commands
{
	public class CustomerAddCommand : IRequest<bool>
	{
		[Required]
		public int Id { get; private set; }
		[Required]
		public string FirstName { get; private set; }
		[Required]
		public string LastName { get; private set; }
		[Required]
		public DateTime BirthDay { get; private set; }

		public string BankAccount{ get; private set; }
		public DateTime? CustomerFrom { get; private set; }
		public bool Active { get; private set; }
		public bool IsNew { get; private set; }

		public CustomerAddCommand(int id, string firstName, string lastName, DateTime birthDay, string bankAccount, DateTime? customerFrom, bool active, bool isNew)
		{
			Id = id;
			FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
			LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
			BirthDay = birthDay;
			BankAccount = bankAccount ?? throw new ArgumentNullException(nameof(bankAccount));
			CustomerFrom = customerFrom ?? throw new ArgumentNullException(nameof(customerFrom));
			Active = active;
			IsNew = isNew;
		}
	}
}
