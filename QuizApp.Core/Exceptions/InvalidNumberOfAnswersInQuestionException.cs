using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class InvalidNumberOfAnswersInQuestionException : DomainException
	{
		public override string Code => "invalid_number_of_answers_in_question";

		public InvalidNumberOfAnswersInQuestionException(int numberOfAnswers) : base($"Number of answers in question invalid: {numberOfAnswers}.")
		{
		}
	}
}
