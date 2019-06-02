using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Application.Customers.Aggregates
{
	public class Entity
	{
		[Key]
		public int Id { get; set; }
	}
}
