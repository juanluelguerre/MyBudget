using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Queries
{
	public class CustomerByIdQueryHandler : IRequestHandler<CustomerByIdQuery, CustomerByIdQueryResult>
	{
		private readonly ILogger _logger;
		private readonly IDataReadonlyRepository<Customer> _repository;

		public CustomerByIdQueryHandler(IDataReadonlyRepository<Customer> repository, ILogger<CustomerByIdQueryHandler> logger)
		{
			_logger = logger;
			_repository = repository;
		}

		public async Task<CustomerByIdQueryResult> Handle(CustomerByIdQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(CustomerByIdQueryResult)}.Handle({request})");

			var customer = await _repository.FindOne(request.Id);
			if (customer != null)
				return new CustomerByIdQueryResult(request.Id, customer.FirstName, customer.LastName, customer.CustomerFrom, customer.BankAccount, customer.Active);
			else
				return null;
		}
	}
}
