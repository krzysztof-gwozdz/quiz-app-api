﻿using Microsoft.Extensions.DependencyInjection;

namespace QuizApp.Api.Extensions
{
	public static class ApplicationInsights
	{
		public static IServiceCollection AddApplicationInsights(this IServiceCollection services)
		{
			services.AddApplicationInsightsTelemetry();
			return services;
		}
	}
}
