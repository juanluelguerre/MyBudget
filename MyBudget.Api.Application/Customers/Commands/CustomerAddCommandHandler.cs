using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Infrastructure;
using MyBudget.Api.Application.Events;

namespace MyBudget.Api.Application.Customers.Commands
{
	public class CustomerAddCommandHandler : RequestHandler<CustomerAddCommand, bool>
	{
		private readonly ILogger _logger;
		private readonly IDataRepository<Customer> _repository;
		private readonly IMediator _mediator;

		public CustomerAddCommandHandler(IMediator mediator, IDataRepository<Customer> repository, ILogger<CustomerAddCommandHandler> logger)
		{
			_logger = logger;
			_repository = repository;
			_mediator = mediator;
		}

		protected override bool Handle(CustomerAddCommand request)
		{
			_logger.LogInformation($"Handle({nameof(CustomerAddCommandHandler)}) -> {request}");

			var customer = Customer.CreateNew(request.Id, request.FirstName, request.LastName, request.CustomerFrom, request.BankAccount);
			var result = _repository.Add(customer);

			_mediator.Publish(new CustomerAddedEvent(request.Id)); 


			return result;
		}		
	}
}
