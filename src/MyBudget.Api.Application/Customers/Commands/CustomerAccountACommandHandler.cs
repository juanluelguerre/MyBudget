using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Domain.Aggregates;
using MyBudget.Api.Application.Customers.Domain.Interfaces;
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
		private readonly IDataService<CustomerAccount> _dataService;
		private readonly IMediator _mediator;

		public CustomerAccountAddCommandHandler(IMediator mediator, IDataService<CustomerAccount> dataService, ILogger<CustomerAccountAddCommandHandler> logger)
		{
			_logger = logger;
			_dataService = dataService;
			_mediator = mediator;
		}

		public async Task<bool> Handle(CustomerAccountAddCommand command, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"Handle({nameof(CustomerAddCommandHandler)}) -> {command}");

			var account = CustomerAccount.CreateNew(command.Id, command.BankAccount, command.MarkAsDefault);
			
			_dataService.ExecuteQuery("UPDATE MarkAsDerault = false FROM CustomerAccounts WHERE Id = @id", new SqlParameter("@id", command.Id));
			var result = _dataService.Add(account);

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
