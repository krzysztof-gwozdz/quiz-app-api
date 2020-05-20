using QuizApp.Shared.Exceptions;

namespace QuizApp.Infrastructure.CosmosDb.Core
{
	public class UnableToFindContainerException : InfrastructureException
	{
		public override string Code => "unable_to_find_container";

		public UnableToFindContainerException(string containerId) : base($"Unable to find container: {containerId}")
		{
		}
	}
}
