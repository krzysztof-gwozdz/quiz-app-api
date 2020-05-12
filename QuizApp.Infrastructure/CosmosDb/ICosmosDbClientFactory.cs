using Microsoft.Azure.Documents;
using System;

namespace QuizApp.Infrastructure.CosmosDb
{
    public interface ICosmosDbClientFactory
    {
        ICosmosDbClient GetClient(string collectionName);
        IDocumentClient GetDocumentClient();
        Uri GetCollectionUri(string collectionName);
    }
}
