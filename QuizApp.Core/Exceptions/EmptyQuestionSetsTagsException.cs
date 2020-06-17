using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionSetsTagsException : DomainException
	{
		public override string Code => "empty_question_set_tag_collection";

		public EmptyQuestionSetsTagsException() : base("Question set tag collection can not be empty.")
		{
		}
	}
}
