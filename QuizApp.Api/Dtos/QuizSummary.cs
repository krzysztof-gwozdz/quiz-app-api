using System;

namespace QuizApp.Api.Dtos
{
	public class QuizSummary
	{
		public Guid QuizId { get; set; }
		public int CorrectAnswers { get; set; }
		public int TotalQuestions { get; set; }
		public QuestionSummary[] QuestionSummaries { get; set; }
	}
}
