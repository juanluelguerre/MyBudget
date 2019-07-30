using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Infrastructure;
using MyBudget.Api.Services;

namespace MyBudget.Api.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly IBudgetService _service;

		public ValuesController(IBudgetService service)
		{
			_service = service;
		}

		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1", "value2" };
		}

		//// GET api/values/5
		//[HttpGet("{id}")]
		//public ActionResult<string> GetById(int id)
		//{
		//	return "value";
		//}

		// POST api/values
		[HttpPost]
		public void Add([FromBody] Budget value)
		{
			_service.Add(value.Amount, value.Description);
		}

		// POST api/values
		[HttpPost]
		public void Remove([FromBody] Budget value)
		{
			_service.Remove(value.Amount, value.Description);
		}

	}
}
