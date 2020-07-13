using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyUserUsernameException : DomainException
	{
		public override string Code => "empty_user_username";

		public EmptyUserUsernameException() : base("User username can not be empty.")
		{
		}
	}
}
