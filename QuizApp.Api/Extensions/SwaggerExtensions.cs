using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace QuizApp.Api.Extensions
{
	public static class SwaggerExtensions
	{
		public static IServiceCollection AddSwaggerWithConfig(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz App API", Version = "v1" });
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme: 'Bearer <TOKEN>'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz App API V1");
				options.RoutePrefix = string.Empty;
			});
			return app;
		}
	}
}
