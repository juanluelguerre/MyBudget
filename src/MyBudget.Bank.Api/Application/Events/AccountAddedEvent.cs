using MediatR;
using System;

namespace MyBudget.Finances.Api.Application.Events
{
	public class AccountAddedEvent : INotification
	{
		public int Id { get; private set; }

		public AccountAddedEvent(int id)
		{
			if (id <= 0) throw new ArgumentException(nameof(id));
			Id = id;
		}		
	}
}
