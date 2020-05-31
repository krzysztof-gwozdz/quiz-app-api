using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionSetIconException : DomainException
	{
		public override string Code => "empty_question_set_icon";

		public EmptyQuestionSetIconException() : base("Question set icon can not be empty.")
		{
		}
	}
}
