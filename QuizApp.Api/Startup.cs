using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QuizApp.Application.Extensions;
using QuizApp.Infrastructure.Extensions;
using QuizApp.Infrastructure.Options;
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
			services.AddRepositories();
			services.AddServices();
			services.AddFactories();
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
