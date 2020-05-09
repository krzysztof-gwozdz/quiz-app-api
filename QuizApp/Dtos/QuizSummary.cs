using System;

namespace QuizApp.Dtos
{
	public class QuizSummary
	{
		public Guid QuizId { get; set; }
		public int CorrectAnswers { get; set; }
		public int TotalQuestions { get; set; }
		public QuestionSummary[] QuestionSummaries { get; set; }
	}
}
