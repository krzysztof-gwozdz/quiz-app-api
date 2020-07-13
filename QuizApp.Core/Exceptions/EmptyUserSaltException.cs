using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyUserSaltException : DomainException
	{
		public override string Code => "empty_user_salt";

		public EmptyUserSaltException() : base("User salt can not be empty.")
		{
		}
	}
}
