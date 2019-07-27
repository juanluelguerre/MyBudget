﻿using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyBudget.Finances.Api.Application.Commands
{
	public class AccountAddCommand : IRequest<bool>
	{
		private const string SEPARATOR = "-"; 		

		[Key]
		public string IBAN { get; private set; }

		public string Entity => IBAN?.Split(SEPARATOR).First();
		public string Office => IBAN?.Split(SEPARATOR)[1];
		public string Control => IBAN?.Split(SEPARATOR)[2];
		public string Number => IBAN?.Split(SEPARATOR).Last();

		public AccountAddCommand(string iban)
		{
			IBAN = iban;
		}
	}
}
