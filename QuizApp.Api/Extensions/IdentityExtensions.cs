using Microsoft.Extensions.DependencyInjection;
using QuizApp.Application;
using QuizApp.Shared;

namespace QuizApp.Api.Extensions
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AddIdentity(this IServiceCollection services)
		{
			var options = services.GetOptions<IdentityOptions>("Identity");
			services.AddSingleton(options);
			return services;
		}
	}
}
