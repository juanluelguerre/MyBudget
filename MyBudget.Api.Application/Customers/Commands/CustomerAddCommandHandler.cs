using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Infrastructure;
using MyBudget.Api.Application.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Commands
{
	public class CustomerAddCommandHandler : IRequestHandler<CustomerAddCommand, bool>
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

		public async Task<bool> Handle(CustomerAddCommand command, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"Handle({nameof(CustomerAddCommandHandler)}) -> {command}");

			var customer = Customer.CreateNew(command.Id, command.FirstName, command.LastName, command.CustomerFrom, command.BankAccount);
			var result = _repository.Add(customer);

			await _mediator.Publish(Apply(command)); 


			return result;
		}

		private CustomerAddedEvent Apply(CustomerAddCommand command)
		{
			if (command == null)
			{
				throw new System.ArgumentNullException(nameof(command));
			}

			return new CustomerAddedEvent(command.Id);			
		}
	}
}
