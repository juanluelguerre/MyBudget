using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Finances.Api.Application.Domain.Aggregates;
using MyBudget.Finances.Api.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Finances.Api.Application.Commands
{
	public class CreateDepositCommandHandler: IRequestHandler<CreateDepositCommand, bool>
	{
		private readonly ILogger _logger;		
		private readonly IMediator _mediator;

		public CreateDepositCommandHandler(IMediator mediator, Logger<CreateDepositCommandHandler> logger)
		{
			_logger = logger;			
			_mediator = mediator;
		}

		public async Task<bool> Handle(CreateDepositCommand command, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"Handle({nameof(AccountAddCommandHandler)}) -> {command}");			

			var account = new Account();

			var result = _dataService.Add(account);

			await _mediator.Publish(Apply(command));

			return result;
		}

		private AccountAddedEvent Apply(AccountAddCommand command)
		{
			if (command == null)
			{
				throw new System.ArgumentNullException(nameof(command));
			}

			return new AccountAddedEvent(command.Id);
		}
	}
}
