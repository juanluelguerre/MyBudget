using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Api.Infrastructure;
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
			.AddSwagger();
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

			app.UseCors("CorsPolicy");

			app.UseSwagger()
				.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBudget v1.0.0");
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

		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.DescribeAllEnumsAsStrings();
				options.SwaggerDoc("v1", new Info
				{
					Version = "v1.0.0",
					Title = "LoanMe Customers API",
					Description = "API to expose LoanMe Custumer logic",
					TermsOfService = ""
				});
			});

			return services;
		}
	}
}
