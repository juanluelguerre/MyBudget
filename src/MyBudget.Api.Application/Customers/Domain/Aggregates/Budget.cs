using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Application.Customers.Domain.Aggregates
{
	public class Budget : AggregateRoot
	{
		[Key]
		public int Id { get; set; }
	}
}
