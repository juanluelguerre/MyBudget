using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Domain.Entities
{
	public class Account
	{
		[Key]
		public string Id { get; set; }
	}
}
