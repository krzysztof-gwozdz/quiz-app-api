using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizApp.Api.Extensions;
using QuizApp.Application.Extensions;
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

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureServices(services => services
					.AddErrorHandling()
					.AddSwaggerWithConfig()
					.AddApi()
					.AddRepositories()
					.AddServices()
					.AddFactories()
					.AddCosmosDb()
				)
				.Configure(app => app
					.UseErrorHandling()
					.UseSwaggerWithConfig()
					.UseApi()
				);
	}
}
