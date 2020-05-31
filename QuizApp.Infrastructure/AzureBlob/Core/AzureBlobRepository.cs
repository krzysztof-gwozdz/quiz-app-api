using Azure.Storage.Blobs;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.AzureBlob.Core
{
	public class AzureBlobRepository : IAzureBlobRepository
	{
		private readonly IAzureBlobClientFactory _azureBlobClientFactory;

		public AzureBlobRepository(IAzureBlobClientFactory azureBlobClientFactory)
		{
			_azureBlobClientFactory = azureBlobClientFactory;
		}

		private string ContainerName
			=> GetType().GetCustomAttribute<AzureBlobRepositoryAttribute>().ContainerName;

		private BlobContainerClient BlobContainerClient =>
			_azureBlobClientFactory.GetBlobContainerClient(ContainerName);

		protected async Task<Stream> GetBlobAsync(string fileName)
		{
			var blobClient = GetBlobClient(fileName);
			var blob = await blobClient.DownloadAsync();
			return blob.Value.Content;
		}

		protected async Task<bool> CheckIfBlobExists(string fileName)
		{
			var blobClient = GetBlobClient(fileName);
			return await blobClient.ExistsAsync();
		}

		protected async Task AddBlobAsync(string fileName, Stream stream)
		{
			var blobClient = GetBlobClient(fileName);
			stream.Position = 0;
			await blobClient.UploadAsync(stream, true);
		}

		private BlobClient GetBlobClient(string fileName)
			=> BlobContainerClient.GetBlobClient(fileName);
	}
}
