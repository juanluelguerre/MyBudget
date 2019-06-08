using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Application.Customers.Commands;
using MyBudget.Api.Application.Customers.Models;
using MyBudget.Api.Application.Customers.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudget.Api.Controllers
{
	// [Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CustomersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Produces(typeof(IEnumerable<CustomerAllViewModel>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Get()
		{
			var customers = await _mediator.Send(new CustomerAllQuery());			
			return Ok(customers);
		}

		[HttpGet("{id}")]
		[Produces(typeof(CustomerByIdViewModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Get(int id)
		{
			var query = new CustomerByIdQuery { Id = id };

			var customer = await _mediator.Send(query);
			if (customer == null)
				return BadRequest(customer);

			return Ok(customer);
		}

		[HttpPost]
		[Produces(typeof(bool))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Post([FromBody] CustomerRequest customer)
		{
			var customerAddCommand = new CustomerAddCommand(customer.Id,
												customer.FirstName,
												customer.LastName,
												customer.BirthDay,
												customer.CustomerFrom,
												customer.Active,
												true);
			var added = await _mediator.Send(customerAddCommand);
			var accountAdded = await AccountSendIfExists(customer.Id, customer.BankAccount, true);
			return Ok(added &&  accountAdded);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Put(int id, [FromBody] CustomerRequest customer)
		{
			if (id == customer.Id && TryValidateModel(customer))
			{
				var customerCommand = new CustomerUpdateCommand(customer.Id,
												customer.FirstName,
												customer.LastName,
												customer.BirthDay);
				var updated = await _mediator.Send(customerCommand);
				var accountAdded = await AccountSendIfExists(customer.Id, customer.BankAccount, true);
				return Ok(updated && accountAdded);
			}

			return BadRequest(customer);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new CustomerDeleteCommand(id);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		private async Task<bool> AccountSendIfExists(int id, string bankAccount, bool markAsDefefault)
		{
			var exists = false;
			var accountAdded = false;
			if (!string.IsNullOrWhiteSpace(bankAccount))
			{
				exists = true;
				var customerAccountCommand = new CustomerAccountAddCommand(id, bankAccount, markAsDefefault);
				accountAdded = await _mediator.Send(customerAccountCommand);
			}

			return accountAdded ^ exists;
		}
	}
}
