using QuizApp.Shared.Exceptions;

namespace QuizApp.Infrastructure.AzureBlob.Exceptions
{
	public class UnableToFindAzureBlobContainerException : InfrastructureException
	{
		public override string Code => "unable_to_find_cosmos_db_container";

		public UnableToFindAzureBlobContainerException(string containerId) : base($"Unable to find Azure Blob container: {containerId}")
		{
		}
	}
}
