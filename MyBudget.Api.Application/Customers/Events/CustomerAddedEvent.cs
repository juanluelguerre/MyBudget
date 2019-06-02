using MediatR;

namespace MyBudget.Api.Application.Events
{
	public class CustomerAddedEvent : INotification
	{
		public int Id { get; private set; }

		public CustomerAddedEvent(int id)
		{
			Id = id;
		}		
	}
}
