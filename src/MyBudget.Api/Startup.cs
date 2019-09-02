using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyBudget.Api.Infrastructure;
using MyBudget.Api.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace MyBudget.Api
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
			services
			.AddCustomMVC()
			.AddCustomDbContext(Configuration)
			.AddCustomServices(Configuration)
			.AddSwagger();

			services.AddCustomHealthCheck(Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}


			app.UseHealthChecks("/hc", new HealthCheckOptions()
			{
				Predicate = _ => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});

			app.UseHealthChecks("/liveness", new HealthCheckOptions
			{
				Predicate = r => r.Name.Contains("self")
			});

			app.UseStaticFiles();
			app.UseCors("CorsPolicy");

			app.UseSwagger()
				.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBudget v1.0.0");
					// c.SwaggerEndpoint("../swagger/v1/swagger.json", "MyBudget v1.0.0");
					//c.RoutePrefix = "swagger";
				});

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}

	internal static class CustomExtensionMethods
	{
		private const string DATABASE_CONNECIONSTRING = "DataBaseConnection";

		public static IServiceCollection AddCustomMVC(this IServiceCollection services)
		{
			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				.AddControllersAsServices();

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
					// builder => builder.AllowAnyOrigin()
					.SetIsOriginAllowed((host) => true)
					.WithMethods(
						"GET",
						"POST",
						"PUT",
						"DELETE",
						"OPTIONS")
					.AllowAnyHeader()
					.AllowCredentials());
			});

			return services;
		}

		public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<DataContext>(options =>
			{
				var dbInMemory = configuration.GetValue<bool>("DBInMemory");

				if (dbInMemory)
				{
					options.UseInMemoryDatabase("Budget",
						(ops) =>
						{

						});
				}
				else
				{
					options.UseSqlServer(configuration.GetConnectionString(DATABASE_CONNECIONSTRING),
										 sqlServerOptionsAction: sqlOptions =>
										 {
											 sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
											 //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
											 sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
										 });

					// Changing default behavior when client evaluation occurs to throw. 
					// Default in EF Core would be to log a warning when client evaluation is performed.
					options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
					//Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
				}
			});

			return services;
		}

		public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IBudgetService, BudgetService>();

			return services;
		}

		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.DescribeAllEnumsAsStrings();
				options.SwaggerDoc("v1", new Info
				{
					Version = "1.0.0",
					Title = "MyBudget API",
					Description = "API to expose MyBudget logic",
					TermsOfService = ""
				});

				//
				// Un-comment to allow authorization
				//
				//options.AddSecurityDefinition("Bearer",
				//   new ApiKeyScheme
				//   {
				//	   In = "header",
				//	   Description = "Please enter into field the word 'Bearer' following by space and JWT",
				//	   Name = "Authorization",
				//	   Type = "apiKey"
				//   });
				//options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
				//	{ "Bearer", Enumerable.Empty<string>() },
				//});
			});

			return services;
		}

		public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
		{
			var hcBuilder = services.AddHealthChecks();

			hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

			return services;
		}
	}
}
