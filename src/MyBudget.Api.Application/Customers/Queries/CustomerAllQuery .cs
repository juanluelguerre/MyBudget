using MediatR;
using System.Collections.Generic;

namespace MyBudget.Api.Application.Customers.Queries
{
	public class CustomerAllQuery : IRequest<IEnumerable<CustomerAllViewModel>>
	{
		
	}
}
