using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Api.ErrorHandling;

namespace QuizApp.Api.Extensions
{
	public static class ErrorHandlingExtensions
	{
		public static IServiceCollection AddErrorHandling(this IServiceCollection services)
		{
			services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
			return services;
		}

		public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
			return app;
		}
	}
}
