namespace QuizApp.Shared.Exceptions
{
	public abstract class DomainException : BaseException
	{
		protected DomainException(string message) : base(message)
		{
		}
	}
}
