using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Shared;

namespace QuizApp.Api.Extensions
{
	public static class HttpContextExtensions
	{
		public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
		{
			AppContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
			return app;
		}
	}
}
