using MediatR;
using System.Collections.Generic;

namespace MyBudget.Customers.Api.Application.Queries
{
	public class CustomerAllQuery : IRequest<IEnumerable<CustomerViewModel>>
	{
		
	}
}
