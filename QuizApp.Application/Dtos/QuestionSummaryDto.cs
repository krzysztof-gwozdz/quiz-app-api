using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionSummaryDto
	{
		public Guid QuestionId { get; set; }
		public string Text { get; set; }
		public AnswerDto[] Answers { get; set; }
		public Guid CorrectAnswerId { get; set; }
		public Guid? PlayerAnswerId { get; set; }
		public bool IsAnswered { get; set; }
		public bool? IsCorrect { get; set; }

		private QuestionSummaryDto()
		{
		}

		public QuestionSummaryDto(Guid questionId, string text, AnswerDto[] answers, Guid correctAnswerId, Guid? playerAnswerId, bool isAnswered, bool? isCorrect)
		{
			QuestionId = questionId;
			Text = text;
			Answers = answers;
			CorrectAnswerId = correctAnswerId;
			PlayerAnswerId = playerAnswerId;
			IsAnswered = isAnswered;
			IsCorrect = isCorrect;
		}
	}
}