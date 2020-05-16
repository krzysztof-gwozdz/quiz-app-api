using System;

namespace QuizApp.Core.Exceptions
{
	public class TooManyQuestionsException : Exception
	{
		public TooManyQuestionsException(int questionCount, int maxQuestionCount) : base($"Too many questions: {questionCount}. Max question count for this question set: {maxQuestionCount}.")
		{
		}
	}
}