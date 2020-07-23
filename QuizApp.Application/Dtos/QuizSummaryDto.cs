using System;

namespace QuizApp.Application.Dtos
{
	public class QuizSummaryDto
	{
		public Guid QuizId { get; set; }
		public Guid QuestionSetId { get; set; }
		public string QuestionSetName { get; set; }
		public int CorrectAnswers { get; set; }
		public int TotalQuestions { get; set; }
		public QuestionSummaryDto[] QuestionSummaries { get; set; }

		public QuizSummaryDto()
		{
		}

		public QuizSummaryDto(Guid quizId, Guid questionSetId, string questionSetName, int correctAnswers, int totalQuestions, QuestionSummaryDto[] questionSummaries)
		{
			QuizId = quizId;
			QuestionSetId = questionSetId;
			QuestionSetName = questionSetName;
			CorrectAnswers = correctAnswers;
			TotalQuestions = totalQuestions;
			QuestionSummaries = questionSummaries;
		}
	}
}
