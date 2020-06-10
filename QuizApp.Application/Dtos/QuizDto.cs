using System;

namespace QuizApp.Application.Dtos
{
	public class QuizDto
	{
		public Guid Id { get; set; }
		public QuestionDto[] Questions { get; set; }

		private QuizDto()
		{
		}

		public QuizDto(Guid id, QuestionDto[] questions)
		{
			Id = id;
			Questions = questions;
		}
	}
}
