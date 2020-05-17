using Microsoft.Azure.Documents;
using QuizApp.Infrastructure.Entities;
using System;

namespace QuizApp.Infrastructure.CosmosDb
{
	public interface ICosmosDbRepository<in T> where T : Entity
	{
		string CollectionId { get; }
		Guid GenerateId(T entity);
		PartitionKey ResolvePartitionKey(string entityId);
	}
}
