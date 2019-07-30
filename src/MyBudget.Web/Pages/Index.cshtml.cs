using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MyBudget.Api.Services;
using System.Threading.Tasks;

namespace MyBudget.Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IBudgetService _service;
		private readonly IOptions<AppSettings> _settings;

		public IndexModel(IBudgetService service, IOptions<AppSettings> settings)
		{
			_service = service;
			_settings = settings;
		}

		[BindProperty]
		public int Total { get; set; } = 0;

		public void OnGet()
		{
		}
		
		public async Task OnPostAddAsync()
		{			
			var result = await _service.Add(1, "Manual deposit");
			if (result)
				Total++;

		}

		public async Task OnPostRemoveAsync()
		{
			if (Total > 0)
			{			
				var result = await _service.Add(-1, "Manual withdrawal");
				if (result)
					Total--;
			}
		}
	}
}
