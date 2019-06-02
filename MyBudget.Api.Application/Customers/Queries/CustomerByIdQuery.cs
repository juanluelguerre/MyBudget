using MediatR;

namespace MyBudget.Api.Application.Customers.Queries
{
	public class CustomerByIdQuery : IRequest<CustomerByIdQueryResult>
	{
		public int Id { get; set; }
	}
}
