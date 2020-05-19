namespace QuizApp.Shared.Exceptions
{
	public abstract class DomainException : BaseException
	{
		public DomainException(string message) : base(message)
		{
		}
	}
}
