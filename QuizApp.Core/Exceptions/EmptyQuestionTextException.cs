using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionTextException : DomainException
	{
		public override string Code => "empty_question_text";

		public EmptyQuestionTextException() : base("Question text can not be empty.")
		{
		}
	}
}
