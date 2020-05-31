using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading;
using System.Threading.Tasks;
using QuizApp.Infrastructure.CosmosDb.Exceptions;

namespace QuizApp.Infrastructure.CosmosDb.Core
{
	public class CosmosDbClient : ICosmosDbClient
	{
		private readonly string _databaseName;
		private readonly string _collectionName;
		private readonly IDocumentClient _documentClient;

		public CosmosDbClient(string databaseName, string collectionName, IDocumentClient documentClient)
		{
			_databaseName = databaseName ?? throw new CosmosDbConfigurationException(nameof(databaseName));
			_collectionName = collectionName ?? throw new CosmosDbConfigurationException(nameof(collectionName));
			_documentClient = documentClient ?? throw new CosmosDbConfigurationException(nameof(documentClient));
		}

		public async Task<Document> ReadDocumentAsync(string documentId, RequestOptions options = null, CancellationToken cancellationToken = default) =>
			await _documentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), options, cancellationToken);

		public async Task<Document> CreateDocumentAsync(object document, RequestOptions options = null, bool disableAutomaticIdGeneration = false, CancellationToken cancellationToken = default) =>
			await _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), document, options, disableAutomaticIdGeneration, cancellationToken);

		public async Task<Document> ReplaceDocumentAsync(string documentId, object document, RequestOptions options = null, CancellationToken cancellationToken = default) =>
			await _documentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), document, options, cancellationToken);

		public async Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null, CancellationToken cancellationToken = default) =>
			await _documentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_databaseName, _collectionName, documentId), options, cancellationToken);
	}
}
