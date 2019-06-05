using Marten;
using Marten.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Data;
using MyBudget.Api.Application.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Events
{
	public class CustomerEventsHandler : 
		INotificationHandler<CustomerAddedEvent>, 
		INotificationHandler<CustomerUpdatedEvent>		
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
			// TODO: save event
			// Use StreamStone (https://github.com/yevhen/Streamstone) to save them into Azure Table Storage


			await Task.CompletedTask;
		}

		public async Task Handle(CustomerUpdatedEvent @event, CancellationToken cancellationToken)
		{
			// TODO: save event
			// Use StreamStone (https://github.com/yevhen/Streamstone) to save them into Azure Table Storage// TODO: save event

			await Task.CompletedTask;
		}		
	}
}
