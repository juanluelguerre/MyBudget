using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Finances.Api.Application.Domain.Aggregates
{
	public class Operation : AggregateRoot
	{
		[Key]
		public int Id { get; private set; }
		public string Description { get; private set; }
		public double Amount { get; private set; }

		public string IBAN1 { get; private set; }
		public string IBAN2 { get; private set; }		

		public Operation(int id, string description, double amount, string iban1, string iban2=null )
		{
			Id = id;
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Amount = amount;
			IBAN1 = iban1 ?? throw new ArgumentNullException(nameof(iban1));
			IBAN2 = iban2;
		}
	}
}