﻿namespace QuizApp.Application.Dtos
{
	public class CreateAnswerDto
	{
		public string Text { get; set; }
		public bool IsCorrect { get; set; }

		public CreateAnswerDto()
		{
		}

		public CreateAnswerDto(string text, bool isCorrect)
		{
			Text = text;
			IsCorrect = isCorrect;
		}
	}
}