﻿using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Core
{
	internal interface ICosmosDbClient
	{
		Task<Document> ReadDocumentAsync(string documentId, RequestOptions options = null, CancellationToken cancellationToken = default);

		Task<Document> CreateDocumentAsync(object document, RequestOptions options = null, bool disableAutomaticIdGeneration = false, CancellationToken cancellationToken = default);

		Task<Document> ReplaceDocumentAsync(string documentId, object document, RequestOptions options = null, CancellationToken cancellationToken = default);

		Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null, CancellationToken cancellationToken = default);
	}
}
