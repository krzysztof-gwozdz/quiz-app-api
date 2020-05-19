namespace QuizApp.Shared.Exceptions
{
	public abstract class AppException : BaseException
	{
		public AppException(string message) : base(message)
		{
		}
	}
}
