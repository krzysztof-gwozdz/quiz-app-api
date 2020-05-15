using System;

namespace QuizApp.Core.Exceptions
{
	public class InvalidNumberOfAnswersInQuestionException : Exception
	{
		public InvalidNumberOfAnswersInQuestionException(int numberOfAnswers) : base($"Number of answers in question invalid: {numberOfAnswers}.")
		{
		}
	}
}
