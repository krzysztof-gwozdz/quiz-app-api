namespace QuizApp.Shared.Exceptions
{
	public abstract class AppException : BaseException
	{
		protected AppException(string message) : base(message)
		{
		}
	}
}
