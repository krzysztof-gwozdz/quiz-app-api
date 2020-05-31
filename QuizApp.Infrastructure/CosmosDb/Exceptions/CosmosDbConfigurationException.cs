using QuizApp.Shared.Exceptions;

namespace QuizApp.Infrastructure.CosmosDb.Exceptions
{
	public class CosmosDbConfigurationException : InfrastructureException
	{
		public override string Code => "cosmos_db_configuration_exception";

		public CosmosDbConfigurationException(string setting) : base($"There was a configuration error with: {setting}")
		{
		}
	}
}
