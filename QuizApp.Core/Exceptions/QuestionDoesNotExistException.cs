using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionDoesNotExistException : Exception
	{
		public QuestionDoesNotExistException(Guid questionId) : base($"Question: {questionId} does not exist.")
		{
		}
	}
}
