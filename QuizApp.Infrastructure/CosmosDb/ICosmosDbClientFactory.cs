using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;

namespace QuizApp.Infrastructure.CosmosDb
{
    public interface ICosmosDbClientFactory
    {
        ICosmosDbClient GetClient(string collectionName);
        DocumentClient GetDocumentClient();
        Uri GetCollectionUri(string collectionName);
    }
}
