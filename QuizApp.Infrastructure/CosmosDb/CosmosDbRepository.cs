using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using QuizApp.Infrastructure.Entities;
using QuizApp.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb
{
	public abstract class CosmosDbRepository<T> : IDocumentCollectionContext<T> where T : Entity
	{
		private readonly ICosmosDbClientFactory _cosmosDbClientFactory;

		protected CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory)
		{
			_cosmosDbClientFactory = cosmosDbClientFactory;
		}

		protected async Task<int> CountDocumentsAsync() => await CountDocumentsAsync((x) => true);

		protected async Task<int> CountDocumentsAsync(Expression<Func<T, bool>> predicate)
		{
			try
			{
				var documentUri = _cosmosDbClientFactory.GetCollectionUri(CollectionName);
				var client = _cosmosDbClientFactory.GetDocumentClient();
				var documentQuery = client.CreateDocumentQuery<T>(documentUri);
				var count = await documentQuery.Where(predicate).CountAsync();
				return count;
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					throw new EntityNotFoundException();
				throw;
			}
		}

		protected async Task<ISet<T>> GetDocumentsAsync() =>
			await GetDocumentsAsync((x) => true);

		protected async Task<ISet<T>> GetDocumentsAsync(Expression<Func<T, bool>> predicate)
		{
			try
			{
				var documentUri = _cosmosDbClientFactory.GetCollectionUri(CollectionName);
				var client = _cosmosDbClientFactory.GetDocumentClient();
				var documentQuery = client
					.CreateDocumentQuery<T>(documentUri, new FeedOptions { EnableCrossPartitionQuery = true })
					.Where(predicate)
					.AsDocumentQuery();

				var entities = new HashSet<T>();
				while (documentQuery.HasMoreResults)
					foreach (T t in await documentQuery.ExecuteNextAsync<T>())
						entities.Add(t);

				return entities;
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					throw new EntityNotFoundException();
				throw;
			}
		}

		protected async Task<T> GetDocumentByIdAsync(Guid id)
		{
			try
			{
				var client = _cosmosDbClientFactory.GetClient(CollectionName);
				var document = await client.ReadDocumentAsync(id.ToString(), new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(id.ToString())
				});

				return JsonConvert.DeserializeObject<T>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					throw new EntityNotFoundException();
				throw;
			}
		}

		protected async Task<bool> CheckIfDocumentExists(Guid id)
		{
			try
			{
				var client = _cosmosDbClientFactory.GetClient(CollectionName);
				var document = await client.ReadDocumentAsync(id.ToString(), new RequestOptions
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

		protected async Task<T> AddDocumentAsync(T entity)
		{
			try
			{
				var client = _cosmosDbClientFactory.GetClient(CollectionName);
				var document = await client.CreateDocumentAsync(entity);
				return JsonConvert.DeserializeObject<T>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.Conflict)
					throw new EntityAlreadyExistsException();
				throw;
			}
		}

		protected async Task UpdateDocumentAsync(T entity)
		{
			try
			{
				var client = _cosmosDbClientFactory.GetClient(CollectionName);
				await client.ReplaceDocumentAsync(entity.Id.ToString(), entity);
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					throw new EntityNotFoundException();
				throw;
			}
		}

		protected async Task DeleteDocumentAsync(Guid id)
		{
			try
			{
				var client = _cosmosDbClientFactory.GetClient(CollectionName);
				await client.DeleteDocumentAsync(id.ToString(), new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(id.ToString())
				});
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					throw new EntityNotFoundException();
				throw;
			}
		}

		public abstract string CollectionName { get; }
		public virtual Guid GenerateId(T entity) => Guid.NewGuid();
		public virtual PartitionKey ResolvePartitionKey(string entityId) => null;
	}
}
