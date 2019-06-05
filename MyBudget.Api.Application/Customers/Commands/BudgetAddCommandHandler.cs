using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Infrastructure;
using MyBudget.Api.Application.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Commands
{
	public class BudgetAddCommandHandler : IRequestHandler<BudgetAddCommand, bool>
	{
		private readonly ILogger _logger;
		private readonly IDataRepository<Budget> _repository;
		private readonly IMediator _mediator;

		public BudgetAddCommandHandler(IMediator mediator, IDataRepository<Budget> repository, ILogger<BudgetAddCommandHandler> logger)
		{
			_logger = logger;
			_repository = repository;
			_mediator = mediator;
		}

		public async Task<bool> Handle(BudgetAddCommand command, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(BudgetAddCommandHandler)}.Handle({command})");

			var result = true;

			await _mediator.Publish(Apply(command));

			return await Task.FromResult(result);
		}

		private BudgetAddedEvent Apply(BudgetAddCommand command)
		{
			if (command == null)
			{
				throw new System.ArgumentNullException(nameof(command));
			}

			return new BudgetAddedEvent();
		}
	}
}
