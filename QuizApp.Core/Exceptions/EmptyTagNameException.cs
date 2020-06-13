using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyTagNameException : DomainException
	{
		public override string Code => "empty_tag_name";

		public EmptyTagNameException() : base("Tag name can not be empty.")
		{
		}
	}
}
