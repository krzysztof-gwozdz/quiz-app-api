using QuizApp.Shared.Exceptions;

namespace QuizApp.Infrastructure.CosmosDb.Exceptions
{
	internal class UnableToFindCosmosDbContainerException : InfrastructureException
	{
		public override string Code => "unable_to_find_cosmos_db_container";

		public UnableToFindCosmosDbContainerException(string containerId) : base($"Unable to find CosmosDb container: {containerId}")
		{
		}
	}
}
