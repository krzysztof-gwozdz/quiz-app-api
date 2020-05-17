using Microsoft.Azure.Documents.Client;
using System;

namespace QuizApp.Infrastructure.CosmosDb.Core
{
    public interface ICosmosDbClientFactory
    {
        DocumentClient DocumentClient { get; }
        ICosmosDbClient GetClient(string collectionName);
        Uri GetCollectionUri(string collectionName);
    }
}
