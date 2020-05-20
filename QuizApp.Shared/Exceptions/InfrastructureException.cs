namespace QuizApp.Shared.Exceptions
{
	public abstract class InfrastructureException : BaseException
	{
		protected InfrastructureException(string message) : base(message)
		{
		}
	}
}
