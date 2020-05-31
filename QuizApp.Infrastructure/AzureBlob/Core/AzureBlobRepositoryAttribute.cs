using System;

namespace QuizApp.Infrastructure.AzureBlob.Core
{
	public class AzureBlobRepositoryAttribute : Attribute
	{
		public string ContainerName { get; }

		public AzureBlobRepositoryAttribute(string containerName)
		{
			ContainerName = containerName;
		}
	}
}