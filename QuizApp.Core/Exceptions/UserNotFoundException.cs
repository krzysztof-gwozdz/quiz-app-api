using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class UserNotFoundException : NotFoundException
	{
		public override string Code => "question_not_found";

		public UserNotFoundException(string userName) : base($"User: {userName} not found.")
		{
		}
	}
}
