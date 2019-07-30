using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyBudget.Api.Services;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace MyBudget.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services
				.AddHealthChecks(Configuration)
				.AddCustomMvc(Configuration)
				// .AddDevspaces()
				.AddHttpClientServices(Configuration);				
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseHealthChecks("/hc", new HealthCheckOptions()
			{
				Predicate = _ => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});


			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHealthChecks("/liveness", new HealthCheckOptions
			{
				Predicate = r => r.Name.Contains("self")
			});

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc();
		}
	}


	static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions();
			services.Configure<AppSettings>(configuration);

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddSession();

			return services;
		}

		public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHealthChecks()
				.AddCheck("self", () => HealthCheckResult.Healthy())
				.AddUrlGroup(new Uri(configuration["ApiUrlHC"]), name: "mybudgetapigw-check", tags: new string[] { "mybudgetapigw" });

			return services;
		}

		public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//register delegating handlers

			//set 5 min as the lifetime for each HttpMessageHandler int the pool
			//services.AddHttpClient("extendedhandlerlifetime")
			//	.SetHandlerLifetime(TimeSpan.FromMinutes(5));
				//.AddDevspacesSupport();

			//add http client services
			services.AddHttpClient<IBudgetService, BudgetService>()
				   .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 5 minutes
				   // .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
				   .AddPolicyHandler(GetRetryPolicy())
				   .AddPolicyHandler(GetCircuitBreakerPolicy());
				   // .AddDevspacesSupport();

			return services;
		}

		static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions
			  .HandleTransientHttpError()
			  .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
			  .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

		}
		static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
		}
	}
}
