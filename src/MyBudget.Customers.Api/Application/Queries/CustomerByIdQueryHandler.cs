using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Customers.Api.Application.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Customers.Api.Application.Queries
{
	public class CustomerByIdQueryHandler : IRequestHandler<CustomerByIdQuery, CustomerViewModel>
	{
		private readonly ILogger _logger;
		private readonly IDataReadonlyRepository _repository;

		public CustomerByIdQueryHandler(IDataReadonlyRepository repository, ILogger<CustomerByIdQueryHandler> logger)
		{
			_logger = logger;
			_repository = repository;
		}

		public async Task<CustomerViewModel> Handle(CustomerByIdQuery query, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(CustomerByIdViewModel)}.Handle({query})");

			var customer = await _repository.FindOne(query.Id);
			if (customer != null)
				return new CustomerViewModel(query.Id,
									   customer.FirstName,
									   customer.LastName,									   
									   customer.BankAccount,
									   customer.MarkAsDefault);
			else
				return null;
		}
	}
}
