using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionNotFoundException : NotFoundException
	{
		public override string Code => "question_not_found";

		public QuestionNotFoundException(Guid questionId) : base($"Question: {questionId} not found.")
		{
		}
	}
}
