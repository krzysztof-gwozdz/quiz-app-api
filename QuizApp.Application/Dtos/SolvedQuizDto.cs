using System;

namespace QuizApp.Application.Dtos
{
	public class SolvedQuizDto
	{
		public Guid QuizId { get; set; }
		public PlayerAnswerDto[] PlayerAnswers { get; set; }
	}
}