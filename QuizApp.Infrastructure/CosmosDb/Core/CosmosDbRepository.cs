using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Core
{
	internal abstract class CosmosDbRepository<TDocument, TId> : ICosmosDbRepository<TDocument, TId>
	{
		private readonly ICosmosDbClientFactory _cosmosDbClientFactory;

		public string CollectionId => GetType().GetCustomAttribute<CosmosDbRepositoryAttribute>().ContainerProperties.Id;
		public virtual PartitionKey ResolvePartitionKey(string id) => new PartitionKey(id);

		protected CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory)
		{
			_cosmosDbClientFactory = cosmosDbClientFactory;
		}

		private ICosmosDbClient CosmosDbClient => _cosmosDbClientFactory.GetClient(CollectionId);
		private DocumentClient DocumentClient => _cosmosDbClientFactory.DocumentClient;
		private Uri DocumentUri => _cosmosDbClientFactory.GetCollectionUri(CollectionId);

		protected async Task<int> CountDocumentsAsync() => await CountDocumentsAsync((x) => true);

		protected async Task<int> CountDocumentsAsync(Expression<Func<TDocument, bool>> predicate)
		{
			try
			{
				var documentQuery = DocumentClient.CreateDocumentQuery<TDocument>(DocumentUri);
				var count = await documentQuery.Where(predicate).CountAsync();
				return count;
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return 0;
				throw;
			}
		}

		protected async Task<ISet<TDocument>> GetDocumentsAsync() =>
			await GetDocumentsAsync((x) => true);

		protected async Task<ISet<TDocument>> GetDocumentsAsync(Expression<Func<TDocument, bool>> predicate)
		{
			try
			{
				var documentQuery = DocumentClient
					.CreateDocumentQuery<TDocument>(DocumentUri, new FeedOptions { EnableCrossPartitionQuery = true })
					.Where(predicate)
					.AsDocumentQuery();

				var documents = new HashSet<TDocument>();
				while (documentQuery.HasMoreResults)
					foreach (TDocument t in await documentQuery.ExecuteNextAsync<TDocument>())
						documents.Add(t);

				return documents;
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return null;
				throw;
			}
		}

		protected async Task<TDocument> GetDocumentByIdAsync(TId id)
		{
			try
			{
				var document = await CosmosDbClient.ReadDocumentAsync(id.ToString(), new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(id.ToString())
				});

				return JsonConvert.DeserializeObject<TDocument>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return default;
				throw;
			}
		}

		protected async Task<bool> CheckIfDocumentExists(TId id)
		{
			try
			{
				var document = await CosmosDbClient.ReadDocumentAsync(id.ToString(), new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(id.ToString())
				});
				return true;
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return false;
				throw;
			}
		}

		protected async Task<TDocument> AddDocumentAsync(TDocument document)
		{
			try
			{
				var createdDocument = await CosmosDbClient.CreateDocumentAsync(document);
				return JsonConvert.DeserializeObject<TDocument>(createdDocument.ToString());
			}
			catch
			{
				throw;
			}
		}

		protected async Task UpdateDocumentAsync(TId id, TDocument document)
		{
			try
			{
				await CosmosDbClient.ReplaceDocumentAsync(id.ToString(), document);
			}
			catch
			{
				throw;
			}
		}

		protected async Task DeleteDocumentAsync(TId id)
		{
			try
			{
				await CosmosDbClient.DeleteDocumentAsync(id.ToString(), new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(id.ToString())
				});
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return;
				throw;
			}
		}
	}
}
