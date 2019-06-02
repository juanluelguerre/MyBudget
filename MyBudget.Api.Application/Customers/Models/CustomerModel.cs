using System;

namespace MyBudget.Api.Application.Customers.Models
{
	public class CustomerModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDay { get; private set; }
		public DateTime? CustomerFrom { get; set; }
		public string BankAccount { get; set; }
		public bool Active { get; private set; }
	}
}
