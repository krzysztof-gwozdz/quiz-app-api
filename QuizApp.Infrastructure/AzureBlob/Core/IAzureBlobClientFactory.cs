using Azure.Storage.Blobs;

namespace QuizApp.Infrastructure.AzureBlob.Core
{
	public interface IAzureBlobClientFactory
	{
		BlobContainerClient GetBlobContainerClient(string containerName);
	}
}