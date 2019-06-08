using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Events;
using MyBudget.Api.Application.Customers.Infrastructure;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Commands
{
	public class CustomerAccountAddCommandHandler : IRequestHandler<CustomerAccountAddCommand, bool>
	{
		private readonly ILogger _logger;
		private readonly IDataRepository<CustomerAccount> _repository;
		private readonly IMediator _mediator;

		public CustomerAccountAddCommandHandler(IMediator mediator, IDataRepository<CustomerAccount> repository, ILogger<CustomerAccountAddCommandHandler> logger)
		{
			_logger = logger;
			_repository = repository;
			_mediator = mediator;
		}

		public async Task<bool> Handle(CustomerAccountAddCommand command, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"Handle({nameof(CustomerAddCommandHandler)}) -> {command}");

			var account = CustomerAccount.CreateNew(command.Id, command.BankAccount, command.MarkAsDefault);
			
			_repository.ExecuteQuery("UPDATE MarkAsDerault = false FROM CustomerAccounts WHERE Id = @id", new SqlParameter("@id", command.Id));
			var result = _repository.Add(account);

			await _mediator.Publish(Apply(command)); 			

			return result;
		}

		private CustomerAccountAddedEvent Apply(CustomerAccountAddCommand command)
		{
			if (command == null)
			{
				throw new System.ArgumentNullException(nameof(command));
			}

			return new CustomerAccountAddedEvent(command.Id, command.BankAccount);			
		}
	}
}
