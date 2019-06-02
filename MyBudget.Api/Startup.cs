﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Api.Application.Customers.Aggregates;
using MyBudget.Api.Application.Customers.Data;
using MyBudget.Api.Application.Customers.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

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
			//services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
			//	.AddAzureADBearer(options => Configuration.Bind("AzureAd", options));


			//services.AddAntiforgery(options =>
			//{
			//	// Set Cookie properties using CookieBuilder properties†.
			//	options.FormFieldName = "AntiforgeryFieldname";
			//	options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
			//	options.SuppressXFrameOptionsHeader = false;
			//});

			

			services.AddDbContext<DataContext>(
				  opt => opt.UseInMemoryDatabase("MyBudget")
					.ConfigureWarnings(cw => cw.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

			//services.AddScoped<IDataReadonlyRepository<Customer>>(
			//	(x) => new DataReadonlyRepository<Customer>($@"Server=(LocalDB)\MSSQLLocalDB; Integrated Security=true ;AttachDbFileName={Directory.GetCurrentDirectory()}\MyBudget.mdf"));

			services.AddScoped<IDataReadonlyRepository<Customer>>(
				(x) => new DataReadonlyRepository<Customer>(Configuration.GetConnectionString("MyBudget")));

			services.AddScoped<DataContext>();
			services.AddScoped<IDataRepository<Customer>, DataRepository<Customer>>();
			services.AddScoped<IDataRepository<Budget>, DataRepository<Budget>>();

			services.AddMediatR(typeof(MyBudget.Api.Application.Customers.Aggregates.Budget));

			// Add Swagger
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1",
					new Info
					{
						Version = "v1.0.0",
						Title = "MyBudget API",
						Description = "API to expose MyBudget logic",
						TermsOfService = ""
					}
				);
			});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

			app.UseSwagger()
				.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBudget v1.0.0");
				});


			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}