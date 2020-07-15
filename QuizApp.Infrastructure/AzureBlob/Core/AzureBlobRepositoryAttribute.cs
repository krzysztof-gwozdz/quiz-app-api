using System;

namespace QuizApp.Infrastructure.AzureBlob.Core
{
	internal class AzureBlobRepositoryAttribute : Attribute
	{
		public string ContainerName { get; }

		public AzureBlobRepositoryAttribute(string containerName)
		{
			ContainerName = containerName;
		}
	}
}