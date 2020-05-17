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
using QuizApp.Core.Factories;
using QuizApp.Core.Repositories;
using System;
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
			services.AddControllers();

			services.AddTransient<IQuestionsRepository, CosmosQuestionsRepository>();
			services.AddTransient<IQuestionSetsRepository, CosmosQuestionSetsRepository>();
			services.AddTransient<IQuizesRepository, CosmosQuizesRepository>();

			services.AddTransient<IQuizesService, QuizesService>();
			services.AddTransient<IQuestionsService, QuestionsService>();
			services.AddTransient<IQuestionSetsService, QuestionSetsService>();

			services.AddTransient<IQuizFactory, QuizFactory>();
			services.AddSingleton<IRandomFactory, RandomFactory>();

			// Add CosmosDb. This verifies database and collections existence.
			services.AddCosmosDb(Configuration.GetSection("CosmosDb").Get<CosmosDbOptions>());

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
