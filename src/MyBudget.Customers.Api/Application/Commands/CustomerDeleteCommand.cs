using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Customers.Api.Application.Commands
{
	public class CustomerDeleteCommand : IRequest<bool>
	{
		[Required]
		public int Id { get; private set; }

		public CustomerDeleteCommand(int id)
		{
			if (id <= 0) throw new ArgumentException(nameof(id));
			Id = id;			
		}
	}
}
