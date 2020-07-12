using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyIdentitySaltException : DomainException
	{
		public override string Code => "empty_identity_salt";

		public EmptyIdentitySaltException() : base("Identity salt can not be empty.")
		{
		}
	}
}
