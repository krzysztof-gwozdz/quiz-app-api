using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class QuestionSetWithSelectedNameAlreadyExistsException : DomainException
	{
		public override string Code => "questio_set_with_selected_name_already_exists";

		public QuestionSetWithSelectedNameAlreadyExistsException(string name) : base($"Question set with name: {name} already exists.")
		{
		}
	}
}
