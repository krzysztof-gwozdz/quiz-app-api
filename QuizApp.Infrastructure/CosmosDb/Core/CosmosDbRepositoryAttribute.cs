using Microsoft.Azure.Cosmos;
using System;

namespace QuizApp.Infrastructure.CosmosDb.Core
{
	public class CosmosDbRepositoryAttribute : Attribute
	{
		public ContainerProperties ContainerProperties { get; }

		public CosmosDbRepositoryAttribute(string containerId, string partitionKeyPath)
		{
			ContainerProperties = new ContainerProperties(containerId, partitionKeyPath);
		}
	}
}
