using QuizApp.Shared.Exceptions;

namespace QuizApp.Infrastructure.AzureBlob.Exceptions
{
	public class AzureBlobConfigurationException : InfrastructureException
	{
		public override string Code => "azure_blob_configuration_exception";

		public AzureBlobConfigurationException(string setting) : base($"There was a configuration error with: {setting}")
		{
		}
	}
}
