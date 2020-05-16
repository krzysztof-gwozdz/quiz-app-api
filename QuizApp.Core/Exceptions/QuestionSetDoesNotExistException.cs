using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionSetDoesNotExistException : Exception
	{
		public QuestionSetDoesNotExistException(Guid questionSetId) : base($"Question set: {questionSetId} does not exist.")
		{
		}
	}
}
