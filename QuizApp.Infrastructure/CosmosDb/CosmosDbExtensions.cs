using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Shared;
using System;
using System.Linq;
using System.Reflection;

namespace QuizApp.Infrastructure.CosmosDb
{
	public static class CosmosDbExtensions
	{
		public static IServiceCollection AddCosmosDb(this IServiceCollection services)
		{
			var options = services.GetOptions<CosmosDbOptions>("CosmosDb");

			var cosmosClient = GetCosmosClient(options);
			var documentClient = GetDocumentClient(options);
			documentClient.OpenAsync().Wait();

			var cosmosDbClientFactory = new CosmosDbClientFactory(options.DatabaseName, GetContainerProperties(), cosmosClient, documentClient);
			cosmosDbClientFactory.EnsureDbSetupAsync().Wait();

			services.AddSingleton<ICosmosDbClientFactory>(cosmosDbClientFactory);

			return services;
		}

		private static CosmosClient GetCosmosClient(CosmosDbOptions options) =>
			new CosmosClient(options.ConnectionString.ServiceEndpoint, options.ConnectionString.AuthKey);

		private static DocumentClient GetDocumentClient(CosmosDbOptions options) =>
			new DocumentClient(new Uri(options.ConnectionString.ServiceEndpoint), options.ConnectionString.AuthKey, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});

		private static ContainerProperties[] GetContainerProperties()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var allTypes = assembly.GetTypes();
			var repositories = allTypes.Where(type =>
				type.BaseType is { } &&
				type.BaseType.IsGenericType &&
				type.BaseType.GetGenericTypeDefinition() == typeof(CosmosDbRepository<,>)
			);
			var containerProperties = repositories.Select(type => type.GetCustomAttribute<CosmosDbRepositoryAttribute>().ContainerProperties);
			return containerProperties.ToArray();
		}
	}
}
