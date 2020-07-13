using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class IdentityNotFoundException : NotFoundException
	{
		public override string Code => "question_not_found";

		public IdentityNotFoundException(string userName) : base($"Identity: {userName} not found.")
		{
		}
	}
}
