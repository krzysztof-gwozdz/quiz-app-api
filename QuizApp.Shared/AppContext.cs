using Microsoft.AspNetCore.Http;

namespace QuizApp.Shared
{
	public class AppContext
	{
		private static IHttpContextAccessor _httpContextAccessor;
		public static HttpContext HttpContext => _httpContextAccessor.HttpContext;
		public static string AppBaseUrl => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";

		public static void Configure(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
	}
}
