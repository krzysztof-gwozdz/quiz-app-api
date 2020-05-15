using System;

namespace QuizApp.Core.Exceptions
{
	public class CorrectAnswerIsNotOneOfAnswersException : Exception
	{
		public CorrectAnswerIsNotOneOfAnswersException(string correctAnswer) : base($"Correct answer: {correctAnswer} is not one of answers.")
		{
		}
	}
}