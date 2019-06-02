using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Infrastructure;

namespace MyBudget.Api.Application.Customers.Commands
{
	public class AddBudgetCommandHandler : RequestHandler<AddBudgetCommand>
	{
		private readonly ILogger _logger;
		private readonly IDataRepository<Budget> _repository;

		public AddBudgetCommandHandler(IDataRepository<Budget> repository, ILogger<AddBudgetCommandHandler> logger)
		{
			_logger = logger;
			_repository = repository;
		}

		protected override void Handle(AddBudgetCommand request)
		{
			_logger.LogInformation($"IngressCommandHandler.Handle(IngressCommand) -> {request}");
		}
	}
}
