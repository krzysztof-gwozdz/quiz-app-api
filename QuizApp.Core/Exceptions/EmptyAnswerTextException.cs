using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyAnswerTextException : DomainException
	{
		public override string Code => "empty_answer_text";

		public EmptyAnswerTextException() : base("Answer text can not be empty.")
		{
		}
	}
}
