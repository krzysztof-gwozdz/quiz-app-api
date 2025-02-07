using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using QuizApp.Api.Extensions;
using QuizApp.Application.Extensions;
using QuizApp.Infrastructure.AzureBlob;
using QuizApp.Infrastructure.CosmosDb;
using QuizApp.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace QuizApp.Api
{
	public class Program
	{
		public static async Task Main(string[] args)
			=> await CreateWebHostBuilder(args)
				.Build()
				.RunAsync();

		private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureServices(services => services
					.AddErrorHandling()
					.AddApplicationInsights()
					.AddAuth()
					.AddSwaggerWithConfig()
					.AddApi()
					.AddRepositories()
					.AddServices()
					.AddFactories()
					.AddCosmosDb()
					.AddAzureBlob()
				)
				.Configure(app => app
					.UseErrorHandling()
					.UseAuth()
					.UseSwaggerWithConfig()
					.UseApi()
				);
	}
}
