using Azure.Storage.Blobs;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Infrastructure.AzureBlob.Core;
using QuizApp.Shared;
using System.Linq;
using System.Reflection;

namespace QuizApp.Infrastructure.AzureBlob
{
	public static class AzureBlobExtensions
	{
		public static IServiceCollection AddAzureBlob(this IServiceCollection services)
		{
			var options = services.GetOptions<AzureBlobOptions>("AzureBlob");

			var blobServiceClient = GetBlobServiceClient(options);

			var azureBlobClientFactory = new AzureBlobClientFactory(blobServiceClient, GetContainerNames());
			azureBlobClientFactory.EnsureDbSetupAsync().Wait();

			services.AddSingleton<IAzureBlobClientFactory>(azureBlobClientFactory);

			return services;
		}

		private static BlobServiceClient GetBlobServiceClient(AzureBlobOptions options) =>
			new BlobServiceClient(options.ConnectionString);

		private static string[] GetContainerNames()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var allTypes = assembly.GetTypes();
			var repositories = allTypes.Where(type =>
				type.BaseType is { } &&
				type.BaseType == typeof(AzureBlobRepository)
			);
			var containers = repositories.Select(type => type.GetCustomAttribute<AzureBlobRepositoryAttribute>().ContainerName);
			return containers.ToArray();
		}
	}
}
