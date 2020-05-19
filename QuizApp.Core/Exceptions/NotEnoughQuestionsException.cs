using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class NotEnoughQuestionsException : DomainException
	{
		public override string Code => "not_enough_questions";

		public NotEnoughQuestionsException(int questionCount, int minQuestionCount) : base($"Not enough question: {questionCount}. Min question count: {minQuestionCount}.")
		{
		}
	}
}