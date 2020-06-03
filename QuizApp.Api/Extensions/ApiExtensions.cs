using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace QuizApp.Api.Extensions
{
	public static class ApiExtensions
	{
		public static IServiceCollection AddApi(this IServiceCollection services)
		{
			services.AddControllers();
			return services;
		}

		public static IApplicationBuilder UseApi(this IApplicationBuilder app)
		{
			app.UseCors(p => p.AllowAnyOrigin());
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

			return app;
		}
	}
}
