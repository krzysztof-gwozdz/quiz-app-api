using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
			if (app.ApplicationServices.GetService<IWebHostEnvironment>().IsDevelopment())
				app.UseDeveloperExceptionPage();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

			return app;
		}
	}
}
