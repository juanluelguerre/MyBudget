using Marten;
using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Events
{
	public class CustomerEventsHandler :
		INotificationHandler<CustomerAddedEvent>,
		INotificationHandler<CustomerUpdatedEvent>,
		INotificationHandler<CustomerAccountAddedEvent>
	{
		private readonly ILogger _logger;
		private readonly IDocumentStore _eventStore;
		private readonly IMediator _mediator;

		public CustomerEventsHandler(IMediator mediator, IDocumentStore eventStore, ILogger<CustomerEventsHandler> logger)
		{
			_logger = logger;
			_eventStore = eventStore;
			_mediator = mediator;
		}

		public async Task Handle(CustomerAddedEvent @event, CancellationToken cancellationToken)
		{
			await Save(@event);			
		}

		public async Task Handle(CustomerUpdatedEvent @event, CancellationToken cancellationToken)
		{
			await Save(@event);			
		}

		public async Task Handle(CustomerAccountAddedEvent @event, CancellationToken cancellationToken)
		{
			await Save(@event);	
		}

		private async Task Save(object @event)
		{
			_logger.LogInformation($"Saving event {@event}");

			using (var session = _eventStore.OpenSession())
			{
				session.Store(@event);
				await session.SaveChangesAsync();
			}

			_logger.LogInformation($"Event saved !");
		}
	}
}
