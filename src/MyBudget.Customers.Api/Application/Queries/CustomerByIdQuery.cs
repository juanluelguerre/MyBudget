using MediatR;

namespace MyBudget.Customers.Api.Application.Queries
{
	public class CustomerByIdQuery : IRequest<CustomerViewModel>
	{
		public int Id { get; set; }
	}
}
