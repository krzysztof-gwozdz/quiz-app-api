using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QuizApp.Api.Extensions;
using QuizApp.Api.Options;
using QuizApp.Application.Services;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var connectionStringsOptions = Configuration.GetSection("ConnectionStrings").Get<ConnectionStringsOptions>();
			var cosmosDbOptions = Configuration.GetSection("CosmosDb").Get<CosmosDbOptions>();
			var (serviceEndpoint, authKey) = connectionStringsOptions.ActiveConnectionStringOptions;
			var (databaseName, collectionData) = cosmosDbOptions;
			var collectionNames = collectionData.Select(c => c.Name).ToList();

			services.AddControllers();

			services.AddTransient<IQuestionSetsRepository, CosmosQuestionSetsRepository>();
			services.AddTransient<IQuizesRepository, FakeQuizesRepository>();

			services.AddTransient<IQuizesService, QuizesService>();
			services.AddTransient<IQuestionSetsService, QuestionSetsService>();

			// Add CosmosDb. This verifies database and collections existence.
			services.AddCosmosDb(serviceEndpoint, authKey, databaseName, collectionNames);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz App API", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.Use(async (context, next) => await RedirectToswagger(context, next));

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz App API V1");
			});

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});


		}

		private async Task RedirectToswagger(HttpContext context, Func<Task> next)
		{
			var url = context.Request.Path.Value;
			if (url == "/")
				context.Request.Path = "/swagger";
			await next();
		}
	}
}
