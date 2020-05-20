using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Api.Extensions
{
	public static class SwaggerExtensions
	{
		public static IServiceCollection AddSwaggerWithConfig(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz App API", Version = "v1" });
			});

			return services;
		}

		public static IApplicationBuilder UseSwaggerWithConfig(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz App API V1");
			});
			app.Use(async (context, next) => await RedirectToSwagger(context, next));

			return app;
		}

		private static async Task RedirectToSwagger(HttpContext context, Func<Task> next)
		{
			var url = context.Request.Path.Value;
			if (url == "/")
				context.Request.Path = "/swagger";
			await next();
		}
	}
}
