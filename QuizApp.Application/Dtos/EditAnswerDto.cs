using System;

namespace QuizApp.Application.Dtos
{
	public class EditAnswerDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public bool IsCorrect { get; set; }

		public EditAnswerDto()
		{
		}

		public EditAnswerDto(Guid id, string text, bool isCorrect)
		{
			Id = id;
			Text = text;
			IsCorrect = isCorrect;
		}
	}
}