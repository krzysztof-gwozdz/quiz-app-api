using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionSetIconNotFoundException : NotFoundException
	{
		public override string Code => "question_set__icon_not_found";

		public QuestionSetIconNotFoundException(Guid questionSetIconId) : base($"Question set icon: {questionSetIconId} not found.")
		{
		}
	}
}
