using Microsoft.Azure.Documents;
using QuizApp.Infrastructure.Entities;
using System;

namespace QuizApp.Infrastructure.CosmosDb
{
    public interface IDocumentCollectionContext<in T> where T : Entity
    {
        string CollectionName { get; }

        Guid GenerateId(T entity);

        PartitionKey ResolvePartitionKey(string entityId);
    }
}
