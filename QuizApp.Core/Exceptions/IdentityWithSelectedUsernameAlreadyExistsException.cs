using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class IdentityWithSelectedUsernameAlreadyExistsException : DomainException
	{
		public override string Code => "identity_with_selected_username_already_exists";

		public IdentityWithSelectedUsernameAlreadyExistsException(string username) : base($"Identity with username: {username} already exists.")
		{
		}
	}
}
