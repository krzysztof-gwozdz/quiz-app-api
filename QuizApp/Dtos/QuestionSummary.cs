using System;

namespace QuizApp.Dtos
{
	public class QuestionSummary
	{
		public Guid QuestionId { get; set; }
		public string Text { get; set; }
		public Answer[] Answers { get; set; }
		public Guid CorrectAnswerId { get; set; }
		public Guid PlayerAnswerId { get; set; }
		public bool IsCorrect { get; set; }
	}
}