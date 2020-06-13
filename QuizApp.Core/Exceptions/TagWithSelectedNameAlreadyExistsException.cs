using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class TagWithSelectedNameAlreadyExistsException : DomainException
	{
		public override string Code => "tag_with_selected_name_already_exists";

		public TagWithSelectedNameAlreadyExistsException(string name) : base($"Tag with name: {name} already exists.")
		{
		}
	}
}
