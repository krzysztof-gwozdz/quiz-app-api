using System;

namespace QuizApp.Application.Dtos
{
	public class QuizQuestionDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public QuizAnswerDto[] Answers { get; set; }
		public string[] Tags { get; set; }

		public QuizQuestionDto()
		{
		}

		public QuizQuestionDto(Guid id, string text, QuizAnswerDto[] answers, string[] tags)
		{
			Id = id;
			Text = text;
			Answers = answers;
			Tags = tags;
		}
	}
}
