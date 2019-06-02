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
		private readonly DataContext _dataContext;

		public CustomerEventsHandler(DataContext dataContext, ILogger<CustomerEventsHandler> logger)
		{
			_logger = logger;
			_dataContext = dataContext;
		}

		public async Task Handle(CustomerAddedEvent notification, CancellationToken cancellationToken)
		{

			// TODO: save event
			// Use StreamStone (https://github.com/yevhen/Streamstone) to save them into Azure Table Storage

			await Task.CompletedTask;
		}

		public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
		{
			// TODO: save event
			// Use StreamStone (https://github.com/yevhen/Streamstone) to save them into Azure Table Storage// TODO: save event

			await Task.CompletedTask;
		}		
	}
}
