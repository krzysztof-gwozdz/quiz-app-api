using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionSetDescriptionException : DomainException
	{
		public override string Code => "empty_question_set_description";

		public EmptyQuestionSetDescriptionException() : base("Question set description can not be empty.")
		{
		}
	}
}
