using System;
using System.Collections.Generic;
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

		protected async Task<ISet<T>> GetAllDocumentsAsync()
		{
			try
			{
				var entities = new HashSet<T>();
				var documentUri = _cosmosDbClientFactory.GetCollectionUri(CollectionName);
				using (var queryable = _cosmosDbClientFactory.GetDocumentClient().CreateDocumentQuery<T>(documentUri).AsDocumentQuery())
				{
					while (queryable.HasMoreResults)
					{
						foreach (T enitity in await queryable.ExecuteNextAsync<T>())
						{
							entities.Add(enitity);
						}
					}
				}

				return entities;
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

		protected async Task DeleteDocumentAsync(T entity)
		{
			try
			{
				var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
				await cosmosDbClient.DeleteDocumentAsync(entity.Id.ToString(), new RequestOptions
				{
					PartitionKey = ResolvePartitionKey(entity.Id.ToString())
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
