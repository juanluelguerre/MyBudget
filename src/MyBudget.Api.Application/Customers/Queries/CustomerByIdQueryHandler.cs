using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Domain.Aggregates;
using MyBudget.Api.Application.Customers.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Customers.Queries
{
	public class CustomerByIdQueryHandler : IRequestHandler<CustomerByIdQuery, CustomerByIdViewModel>
	{
		private readonly ILogger _logger;
		private readonly IDataReadonlyService<Customer> _repository;

		public CustomerByIdQueryHandler(IDataReadonlyService<Customer> repository, ILogger<CustomerByIdQueryHandler> logger)
		{
			_logger = logger;
			_repository = repository;
		}

		public async Task<CustomerByIdViewModel> Handle(CustomerByIdQuery query, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(CustomerByIdViewModel)}.Handle({query})");

			var customer = await _repository.FindOne(query.Id);
			if (customer != null)
				return new CustomerByIdViewModel(query.Id,
									   customer.FirstName,
									   customer.LastName,
									   customer.CustomerFrom,
									   customer.BankAccounts.First(b => b.MarkAsDefault).BankAccount,
									   customer.Active);
			else
				return null;
		}
	}
}
