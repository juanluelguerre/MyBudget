using MediatR;

namespace MyBudget.Api.Application.Customers.Queries
{
	public class CustomerByIdQuery : IRequest<CustomerByIdViewModel>
	{
		public int Id { get; set; }
	}
}
