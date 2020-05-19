using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using QuizApp.Shared.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Core
{
	public class CosmosDbClientFactory : ICosmosDbClientFactory
	{
		private readonly string _databaseName;
		private readonly ContainerProperties[] _containerProperties;
		private readonly CosmosClient _cosmosClient;
		public DocumentClient DocumentClient { get; }

		public CosmosDbClientFactory(string databaseName, ContainerProperties[] containerProperties, CosmosClient cosmosClient, DocumentClient documentClient)
		{
			_databaseName = databaseName ?? throw new UnableToFindContainerException(nameof(databaseName));
			_containerProperties = containerProperties ?? throw new UnableToFindContainerException(nameof(containerProperties));
			_cosmosClient = cosmosClient ?? throw new UnableToFindContainerException(nameof(cosmosClient));
			DocumentClient = documentClient ?? throw new UnableToFindContainerException(nameof(documentClient));
		}

		public ICosmosDbClient GetClient(string containerId)
		{
			if (!_containerProperties.Any(x => x.Id == containerId))
				throw new UnableToFindContainerException(containerId);
			return new CosmosDbClient(_databaseName, containerId, DocumentClient);
		}

		public Uri GetCollectionUri(string containerId) =>
			UriFactory.CreateDocumentCollectionUri(_databaseName, containerId);

		public async Task EnsureDbSetupAsync()
		{
			var databaseResponse = (await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName)).Database;
			foreach (var containerProperties in _containerProperties)
				await databaseResponse.CreateContainerIfNotExistsAsync(containerProperties);
		}
	}
}
