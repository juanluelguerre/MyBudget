using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBudget.Finances.Api.Application.Domain.Aggregates
{
	public class Account : AggregateRoot
	{		
		[Key]
		public int Id { get; set; }
		// public string Name { get; private set; }

		[MaxLength(4)]
		public string Entity { get; private set; }
		[MaxLength(4)]
		public string Office { get; private set; }
		[MaxLength(2)]
		public string Control { get; private set; }
		[MaxLength(10)]
		public string Number { get; private set; }

		public double Amount { get; private set; }

		[NotMapped]
		public string IBAN => $"{Entity}-{Office}-{Control}-{Number}";

		public List<Operation> Operations { get; private set; }

		public Account(string entity, string office, string control, string account, double amount)
		{			
			Entity = entity;
			Office = office;
			Control = control;
			Number = account;
			Amount = amount;

			if (!ValidAccountNumber())
			{
				throw new ArgumentException($"Invalid account number: '{AccountNumber}'");
			}
		}

		private bool ValidAccountNumber()
		{
			return true;
		}
	}
}

