using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Application.Customers.Aggregates
{
	public class AggregateRoot
	{
		[Key]
		public int Id { get; set; }
	}
}
