using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class TooManyQuestionsException : DomainException
	{
		public override string Code => "too_many_questions";

		public TooManyQuestionsException(int questionCount, int maxQuestionCount) : base($"Too many questions: {questionCount}. Max question count for this question set: {maxQuestionCount}.")
		{
		}
	}
}