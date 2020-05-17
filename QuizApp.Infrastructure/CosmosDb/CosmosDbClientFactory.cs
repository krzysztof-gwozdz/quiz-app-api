using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb
{
	public class CosmosDbClientFactory : ICosmosDbClientFactory
	{
		private readonly string _databaseName;
		private readonly ContainerProperties[] _containerProperties;
		private readonly CosmosClient _cosmosClient;
		public DocumentClient DocumentClient { get; }

		public CosmosDbClientFactory(string databaseName, ContainerProperties[] containerProperties, CosmosClient cosmosClient, DocumentClient documentClient)
		{
			_databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
			_containerProperties = containerProperties ?? throw new ArgumentNullException(nameof(containerProperties));
			_cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
			DocumentClient = documentClient ?? throw new ArgumentNullException(nameof(documentClient));
		}

		public ICosmosDbClient GetClient(string containerId)
		{
			if (!_containerProperties.Any(x => x.Id == containerId))
				throw new ArgumentException($"Unable to find container: {containerId}");
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
