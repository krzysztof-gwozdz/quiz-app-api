using System;

namespace QuizApp.Core.Exceptions
{
	public class AnswerIsNotFromQuestionException : Exception
	{
		public AnswerIsNotFromQuestionException(Guid answerId, Guid questionId) : base($"Answer: {answerId} is not from question: {questionId}.")
		{
		}
	}
}
