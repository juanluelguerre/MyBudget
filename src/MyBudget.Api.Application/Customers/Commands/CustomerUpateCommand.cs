using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Application.Customers.Commands
{
	public class CustomerUpdateCommand : IRequest<bool>
	{
		[Required]
		public int Id { get; private set; }
		[Required]
		public string FirstName { get; private set; }
		[Required]
		public string LastName { get; private set; }
		[Required]
		public DateTime BirthDay { get; private set; }

		public CustomerUpdateCommand(int id, string firstName, string lastName, DateTime birthDay)
		{
			Id = id;
			FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
			LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
			BirthDay = birthDay;
		}
	}
}
