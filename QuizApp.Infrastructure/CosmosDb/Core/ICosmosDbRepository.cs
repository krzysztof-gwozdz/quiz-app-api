using Microsoft.Azure.Documents;

namespace QuizApp.Infrastructure.CosmosDb.Core
{
	internal interface ICosmosDbRepository<T, TId>
	{
		string CollectionId { get; }
		PartitionKey ResolvePartitionKey(string id);
	}
}
