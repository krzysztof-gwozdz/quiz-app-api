using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyIdentityPasswordHashException : DomainException
	{
		public override string Code => "empty_identity_password_hash";

		public EmptyIdentityPasswordHashException() : base("Identity password hash can not be empty.")
		{
		}
	}
}
