using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class QuizNotFoundException : NotFoundException
	{
		public override string Code => "quiz_not_found";

		public QuizNotFoundException(Guid quizId) : base($"Quiz: {quizId} not found.")
		{
		}
	}
}
