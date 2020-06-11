using System;

namespace QuizApp.Application.Dtos
{
	public class QuizSummaryDto
	{
		public Guid QuizId { get; set; }
		public int CorrectAnswers { get; set; }
		public int TotalQuestions { get; set; }
		public QuestionSummaryDto[] QuestionSummaries { get; set; }

		public QuizSummaryDto()
		{
		}

		public QuizSummaryDto(Guid quizId, int correctAnswers, int totalQuestions, QuestionSummaryDto[] questionSummaries)
		{
			QuizId = quizId;
			CorrectAnswers = correctAnswers;
			TotalQuestions = totalQuestions;
			QuestionSummaries = questionSummaries;
		}
	}
}
