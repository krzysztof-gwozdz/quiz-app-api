using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyTagDescriptionException : DomainException
	{
		public override string Code => "empty_tag_description";

		public EmptyTagDescriptionException() : base("Tag description can not be empty.")
		{
		}
	}
}
