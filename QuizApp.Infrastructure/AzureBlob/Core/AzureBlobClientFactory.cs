using Azure.Storage.Blobs;
using QuizApp.Infrastructure.AzureBlob.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.AzureBlob.Core
{
	internal class AzureBlobClientFactory : IAzureBlobClientFactory
	{
		private readonly BlobServiceClient _blobServiceClient;
		private readonly string[] _containerNames;

		public AzureBlobClientFactory(BlobServiceClient blobServiceClient, string[] containerNames)
		{
			_blobServiceClient = blobServiceClient ?? throw new AzureBlobConfigurationException(nameof(containerNames));
			_containerNames = containerNames ?? throw new AzureBlobConfigurationException(nameof(containerNames));
		}

		public BlobContainerClient GetBlobContainerClient(string containerName)
		{
			if (!_containerNames.Contains(containerName))
				throw new UnableToFindAzureBlobContainerException(containerName);
			return _blobServiceClient.GetBlobContainerClient(containerName);
		}

		public async Task EnsureDbSetupAsync()
		{
			var blobContainers = _blobServiceClient.GetBlobContainers();
			foreach (var containerName in _containerNames)
			{
				if (!blobContainers.Any(blobContainer => blobContainer.Name == containerName))
					await _blobServiceClient.CreateBlobContainerAsync(containerName);
			}
		}
	}
}
