using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class AnswerIsNotFromQuestionException : DomainException
	{
		public override string Code => "answer_is_not_from_question";

		public AnswerIsNotFromQuestionException(Guid answerId, Guid questionId) : base($"Answer: {answerId} is not from question: {questionId}.")
		{
		}
	}
}
