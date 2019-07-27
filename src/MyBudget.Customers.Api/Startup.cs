﻿using Marten;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Customers.Api.Application.Domain.Aggregates;
using MyBudget.Customers.Api.Application.Domain.Interfaces;
using MyBudget.Customers.Api.Application.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace MyBudget.Customers.Api
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

			//services.AddDbContext<DataContext>(
			//	  opt => opt.UseInMemoryDatabase("MyBudget")
			//		.ConfigureWarnings(cw => cw.Ignore(InMemoryEventId.TransactionIgnoredWarning)));			

			// EF Connnection
			services.AddDbContext<DataContext>(
				(ops) => ops.UseMySQL(Configuration.GetConnectionString("MyBudget")));

			// Dapper Connection
			services.AddScoped<IDataReadonlyRepository>(
				(_) => new DataReadonlyRepository(Configuration.GetConnectionString("MyBudget")));

			services.AddScoped((x) => GetDocumentStore());

			services.AddScoped<DataContext>();
			services.AddScoped<IDataService<Customer>, DataRepository<Customer>>();
			services.AddScoped<IDataService<CustomerAccount>, DataRepository<CustomerAccount>>();
			// services.AddScoped<IDataService<Budget>, DataRepository<Budget>>();

			services.AddMediatR(typeof(Customer));

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

		private IDocumentStore GetDocumentStore()
		{
			const string SCHEMA_NAME = "EventStore";

			var store = DocumentStore.For(options =>
			{
				options.Connection(Configuration.GetConnectionString("EventStore"));
				options.AutoCreateSchemaObjects = AutoCreate.All;
				options.DatabaseSchemaName = SCHEMA_NAME;
				options.Events.DatabaseSchemaName = SCHEMA_NAME;

				// options.Events.InlineProjections.AggregateStreamsWith<XXXX>();
			});

			return store;
		}
	}
}
