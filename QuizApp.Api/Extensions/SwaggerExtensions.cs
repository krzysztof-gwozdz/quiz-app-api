using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
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
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme: 'Bearer <TOKEN>'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{{
					new OpenApiSecurityScheme
					{
						Description = "Adds token to header",
						Name = "Authorization",
						Type = SecuritySchemeType.Http,
						In = ParameterLocation.Header,
						Scheme = "bearer",
						Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
					},
					new List<string>()
				}});
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
