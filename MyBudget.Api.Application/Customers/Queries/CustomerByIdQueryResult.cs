﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudget.Api.Application.Customers.Queries
{
	public class CustomerByIdQueryResult
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public DateTime? CustomerFrom { get; set; }
		[Required]
		public string BankAccount { get; set; }
		public bool Active { get; set; }

		public CustomerByIdQueryResult(int id, string firstName, string lastName, DateTime? customerFrom, string bankAccount, bool active)
		{
			Id = id;
			FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
			LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
			CustomerFrom = customerFrom ?? throw new ArgumentNullException(nameof(customerFrom));
			BankAccount = bankAccount ?? throw new ArgumentNullException(nameof(bankAccount));
			Active = active;
		}
	}
}