using Azure.Storage.Blobs;

namespace QuizApp.Infrastructure.AzureBlob.Core
{
	internal interface IAzureBlobClientFactory
	{
		BlobContainerClient GetBlobContainerClient(string containerName);
	}
}