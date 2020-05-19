using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionSetNotFoundException : NotFoundException
	{
		public override string Code => "question_set_not_found";

		public QuestionSetNotFoundException(Guid questionSetId) : base($"Question set: {questionSetId} not found.")
		{
		}
	}
}
