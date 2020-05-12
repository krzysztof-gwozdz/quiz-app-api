using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionSummaryDto
	{
		public Guid QuestionId { get; set; }
		public string Text { get; set; }
		public AnswerDto[] Answers { get; set; }
		public Guid CorrectAnswerId { get; set; }
		public Guid PlayerAnswerId { get; set; }
		public bool IsCorrect { get; set; }
	}
}