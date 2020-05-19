namespace QuizApp.Shared.Exceptions
{
	public class UnableToFindContainerException : InfrastructureException
	{
		public override string Code => "unable_to_find_container";

		public UnableToFindContainerException(string containerId) : base($"Unable to find container: {containerId}")
		{
		}
	}
}
