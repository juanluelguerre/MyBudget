using MediatR;
using System;

namespace MyBudget.Api.Application.Events
{
	public class CustomerAddedEvent : INotification
	{
		public int Id { get; private set; }

		public CustomerAddedEvent(int id)
		{
			if (id <= 0) throw new ArgumentException(nameof(id));
			Id = id;
		}		
	}
}
