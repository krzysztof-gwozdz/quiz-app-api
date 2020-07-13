using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyUserPasswordHashException : DomainException
	{
		public override string Code => "empty_user_password_hash";

		public EmptyUserPasswordHashException() : base("User password hash can not be empty.")
		{
		}
	}
}
