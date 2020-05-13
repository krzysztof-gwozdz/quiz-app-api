using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using QuizApp.Infrastructure.Entities;
using QuizApp.Infrastructure.Exceptions;

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
				var count = await _cosmosDbClientFactory.GetDocumentClient().CreateDocumentQuery<T>(documentUri).Where(predicate).CountAsync();
				return count;
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException();
				}

				throw;
			}
		}

		protected async Task<ISet<T>> GetDocumentsAsync() => await GetDocumentsAsync((x) => true);

		protected async Task<ISet<T>> GetDocumentsAsync(Expression<Func<T, bool>> predicate)
		{
			try
			{
				var entities = new HashSet<T>();
				var documentUri = _cosmosDbClientFactory.GetCollectionUri(CollectionName);
				return _cosmosDbClientFactory.GetDocumentClient().CreateDocumentQuery<T>(documentUri, new FeedOptions { EnableCrossPartitionQuery = true }).Where(predicate).ToHashSet();
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException();
				}

				throw;
			}
		}

		protected async Task<T> GetDocumentByIdAsync(Guid id)
		{
			try
			{
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				var document = await cosmosDbClient.ReadDocumentAsync(id.ToString(), new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(id.ToString())
				});

				return JsonConvert.DeserializeObject<T>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException();
				}

				throw;
			}
		}

		protected async Task<T> AddDocumentAsync(T entity)
		{
			try
			{
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				var document = await cosmosDbClient.CreateDocumentAsync(entity);
				return JsonConvert.DeserializeObject<T>(document.ToString());
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.Conflict)
				{
					throw new EntityAlreadyExistsException();
				}

				throw;
			}
		}

		protected async Task UpdateDocumentAsync(T entity)
		{
			try
			{
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				await cosmosDbClient.ReplaceDocumentAsync(entity.Id.ToString(), entity);
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException();
				}

				throw;
			}
		}

		protected async Task DeleteDocumentAsync(Guid id)
		{
			try
			{
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				await cosmosDbClient.DeleteDocumentAsync(id.ToString(), new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(id.ToString())
				});
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					throw new EntityNotFoundException();
				}

				throw;
			}
		}

		public abstract string CollectionName { get; }
		public virtual Guid GenerateId(T entity) => Guid.NewGuid();
		public virtual PartitionKey ResolvePartitionKey(string entityId) => null;
	}
}
