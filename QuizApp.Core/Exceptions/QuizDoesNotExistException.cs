using System;

namespace QuizApp.Core.Exceptions
{
	public class QuizDoesNotExistException : Exception
	{
		public QuizDoesNotExistException(Guid quizId) : base($"Quiz: {quizId} does not exist.")
		{
		}
	}
}
