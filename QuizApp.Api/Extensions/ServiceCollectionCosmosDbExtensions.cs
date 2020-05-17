using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QuizApp.Api.Options;
using QuizApp.Infrastructure.CosmosDb;

namespace QuizApp.Api.Extensions
{
	public static class ServiceCollectionCosmosDbExtensions
	{
		public static IServiceCollection AddCosmosDb(this IServiceCollection services, CosmosDbOptions options)
		{
			var documentClient = new DocumentClient(options.ConnectionString.ServiceEndpoint, options.ConnectionString.AuthKey, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});
			documentClient.OpenAsync().Wait();

			var cosmosDbClientFactory = new CosmosDbClientFactory(options.DatabaseName, options.CollectionNames, documentClient);
			cosmosDbClientFactory.EnsureDbSetupAsync().Wait();

			services.AddSingleton<ICosmosDbClientFactory>(cosmosDbClientFactory);

			return services;
		}
	}
}
