using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyIdentityUsernameException : DomainException
	{
		public override string Code => "empty_identity_username";

		public EmptyIdentityUsernameException() : base("Identity username can not be empty.")
		{
		}
	}
}
