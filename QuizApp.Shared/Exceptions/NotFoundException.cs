namespace QuizApp.Shared.Exceptions
{
	public abstract class NotFoundException : DomainException
	{
		public NotFoundException(string message) : base(message)
		{
		}
	}
}
