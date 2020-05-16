using System;

namespace QuizApp.Core.Exceptions
{
	public class NotEnoughQuestionsException : Exception
	{
		public NotEnoughQuestionsException(int questionCount, int minQuestionCount) : base($"Not enough question: {questionCount}. Min question count: {minQuestionCount}.")
		{
		}
	}
}