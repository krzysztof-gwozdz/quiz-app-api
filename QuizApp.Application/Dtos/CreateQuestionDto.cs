using System;

namespace QuizApp.Application.Dtos
{
	public class CreateQuestionDto
	{
		public string Text { get; set; }
		public CreateAnswerDto[] Answers { get; set; }
		public Guid QuestionSetId { get; set; }

		public CreateQuestionDto()
		{
		}

		public CreateQuestionDto(string text, CreateAnswerDto[] answers, Guid questionSetId)
		{
			Text = text;
			Answers = answers;
			QuestionSetId = questionSetId;
		}
	}
}
