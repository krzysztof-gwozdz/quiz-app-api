using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public AnswerDto[] Answers { get; set; }
		public Guid QuestionSetId { get; set; }

		private QuestionDto()
		{
		}

		public QuestionDto(Guid id, string text, AnswerDto[] answers, Guid questionSetId)
		{
			Id = id;
			Text = text;
			Answers = answers;
			QuestionSetId = questionSetId;
		}
	}
}
