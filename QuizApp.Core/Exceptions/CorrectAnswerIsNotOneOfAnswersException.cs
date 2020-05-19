using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class CorrectAnswerIsNotOneOfAnswersException : DomainException
	{
		public override string Code => "correct_answer_is_not_one_of_answers";

		public CorrectAnswerIsNotOneOfAnswersException(string correctAnswer) : base($"Correct answer: {correctAnswer} is not one of answers.")
		{
		}
	}
}