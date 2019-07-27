using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBudget.Finances.Api.Application.Domain.Aggregates
{
	public class Product : AggregateRoot
	{
		[Key]
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		
		public Product(int id, string name, string description)
		{
			Id = id;
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Description = description ?? throw new ArgumentNullException(nameof(description));			
		}
	}
}
