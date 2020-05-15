using System;

namespace QuizApp.Core.Exceptions
{
	public class SelectedQuestionSetDoesNotExistException : Exception
	{
		public SelectedQuestionSetDoesNotExistException(Guid questionSetId) : base($"Selected question set: {questionSetId} does not exist.")
		{
		}
	}
}
