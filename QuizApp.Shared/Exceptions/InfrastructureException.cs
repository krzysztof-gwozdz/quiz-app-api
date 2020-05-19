namespace QuizApp.Shared.Exceptions
{
	public abstract class InfrastructureException : BaseException
	{
		public InfrastructureException(string message) : base(message)
		{
		}
	}
}
