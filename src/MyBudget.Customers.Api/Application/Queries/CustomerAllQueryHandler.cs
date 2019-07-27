using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Customers.Api.Application.Domain.Interfaces;
using MyBudget.Customers.Api.Application.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Queries
{
	public class CustomerAllQueryHandler : IRequestHandler<CustomerAllQuery, IEnumerable<CustomerViewModel>>
	{
		private readonly ILogger _logger;
		private readonly IDataReadonlyRepository _repository;

		public CustomerAllQueryHandler(IDataReadonlyRepository repository, ILogger<CustomerAllQueryHandler> logger)
		{
			_logger = logger;
			_repository = repository;
		}

		public async Task<IEnumerable<CustomerViewModel>> Handle(CustomerAllQuery query, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(CustomerAllQueryHandler)}.Handle({query})");			
			var customers = await _repository.FindAll();			
			return customers;
		}
	}
}
