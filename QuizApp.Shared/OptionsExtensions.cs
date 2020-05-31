using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QuizApp.Shared
{
	public static class OptionsExtensions
	{
		public static T GetOptions<T>(this IServiceCollection services, string settingsSectionName)
		{
			using var serviceProvider = services.BuildServiceProvider();
			var configuration = serviceProvider.GetService<IConfiguration>();
			return configuration.GetSection(settingsSectionName).Get<T>();
		}
	}
}
