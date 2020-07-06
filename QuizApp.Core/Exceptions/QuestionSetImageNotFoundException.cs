using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionSetImageNotFoundException : NotFoundException
	{
		public override string Code => "question_set_image_not_found";

		public QuestionSetImageNotFoundException(Guid questionSetImageId) : base($"Question set image: {questionSetImageId} not found.")
		{
		}
	}
}
