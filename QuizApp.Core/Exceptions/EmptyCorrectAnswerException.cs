using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyCorrectAnswerException : DomainException
	{
		public override string Code => "empty_correct_answer";

		public EmptyCorrectAnswerException() : base("Correct answer can not be empty.")
		{
		}
	}
}
