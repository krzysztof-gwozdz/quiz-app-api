using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionTagsException : DomainException
	{
		public override string Code => "empty_question_tag_collection";

		public EmptyQuestionTagsException() : base("Question tag collection can not be empty.")
		{
		}
	}
}
