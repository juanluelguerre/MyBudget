using MediatR;
using Microsoft.Extensions.Logging;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Infrastructure;
using MyBudget.Api.Application.Customers.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.Api.Application.Queries
{
	public class CustomerAllQueryHandler : IRequestHandler<CustomerAllQuery, IEnumerable<CustomerAllViewModel>>
	{
		private readonly ILogger _logger;
		private readonly IDataReadonlyRepository<Customer> _repository;

		public CustomerAllQueryHandler(IDataReadonlyRepository<Customer> repository, ILogger<CustomerAllQueryHandler> logger)
		{
			_logger = logger;
			_repository = repository;
		}

		public async Task<IEnumerable<CustomerAllViewModel>> Handle(CustomerAllQuery query, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"{nameof(CustomerAllQueryHandler)}.Handle({query})");
			// var customers = _repository.FindAll("SELECT Id, FirstName, LastName FROM Customers").Result;
			var customers = await _repository.FindAll();
			var list = customers.Select(c => new CustomerAllViewModel(
				c.Id, 
				$"{c.FirstName} {c.LastName}", 
				c.BankAccounts.First(b => b.MarkAsDefault).BankAccount));

			return list;
		}
	}
}
