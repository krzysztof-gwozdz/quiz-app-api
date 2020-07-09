using System;

namespace QuizApp.Application.Dtos
{
	public class QuizAnswerDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public bool IsCorrect { get; set; }

		public QuizAnswerDto()
		{
		}

		public QuizAnswerDto(Guid id, string text, bool isCorrect)
		{
			Id = id;
			Text = text;
			IsCorrect = isCorrect;
		}
	}
}