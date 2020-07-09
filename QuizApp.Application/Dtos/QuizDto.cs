using System;

namespace QuizApp.Application.Dtos
{
	public class QuizDto
	{
		public Guid Id { get; set; }
		public QuizQuestionDto[] Questions { get; set; }

		public QuizDto()
		{
		}

		public QuizDto(Guid id, QuizQuestionDto[] questions)
		{
			Id = id;
			Questions = questions;
		}
	}
}
