using System;

namespace QuizApp.Application.Dtos
{
	public class AnswerDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public bool IsCorrect { get; set; }

		public AnswerDto()
		{
		}

		public AnswerDto(Guid id, string text, bool isCorrect)
		{
			Id = id;
			Text = text;
			IsCorrect = isCorrect;
		}
	}
}