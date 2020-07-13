using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class UserWithSelectedUsernameAlreadyExistsException : DomainException
	{
		public override string Code => "user_with_selected_username_already_exists";

		public UserWithSelectedUsernameAlreadyExistsException(string username) : base($"User with username: {username} already exists.")
		{
		}
	}
}
