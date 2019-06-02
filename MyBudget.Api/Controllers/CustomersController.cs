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
		[Produces(typeof(IEnumerable<CustomerAllQueryResult>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get()
		{
			var customers = await _mediator.Send(new CustomerAllQuery());			
			return Ok(customers);
		}

		[HttpGet("{id}")]
		[Produces(typeof(CustomerByIdQueryResult))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get(int id)
		{
			var query = new CustomerByIdQuery { Id = id };

			var customer = await _mediator.Send(query);
			if (customer == null)
				return BadRequest(customer);

			return Ok(customer);
		}

		[HttpPost]
		[Produces(typeof(CustomerModel))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Post([FromBody] CustomerModel customer)
		{
			var customerCommand = new CustomerAddCommand(customer.Id,
												customer.FirstName,
												customer.LastName,
												customer.BirthDay,
												customer.BankAccount,
												customer.CustomerFrom,
												customer.Active,
												true);			
			var result = await _mediator.Send(customerCommand);
			return Ok(result);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public void Put(int id, [FromBody] string value)
		{
			// For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public void Delete(int id)
		{
			// For more information on protecting this API from Cross Site Request Forgery (CSRF) attacks, see https://go.microsoft.com/fwlink/?LinkID=717803
		}
	}
}
