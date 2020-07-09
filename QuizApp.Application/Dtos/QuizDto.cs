using System;

namespace QuizApp.Application.Dtos
{
	public class QuizDto
	{
		public Guid Id { get; set; }
		public Guid QuestionSetId { get; set; }
		public QuizQuestionDto[] Questions { get; set; }

		public QuizDto()
		{
		}

		public QuizDto(Guid id, Guid questionSetId, QuizQuestionDto[] questions)
		{
			Id = id;
			QuestionSetId = questionSetId;
			Questions = questions;
		}
	}
}
