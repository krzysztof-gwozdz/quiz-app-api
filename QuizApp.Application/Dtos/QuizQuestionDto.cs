using System;

namespace QuizApp.Application.Dtos
{
	public class QuizQuestionDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public QuizAnswerDto[] Answers { get; set; }

		public QuizQuestionDto()
		{
		}

		public QuizQuestionDto(Guid id, string text, QuizAnswerDto[] answers)
		{
			Id = id;
			Text = text;
			Answers = answers;
		}
	}
}
