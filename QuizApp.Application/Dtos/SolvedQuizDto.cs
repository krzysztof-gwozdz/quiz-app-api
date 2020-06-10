using System;

namespace QuizApp.Application.Dtos
{
	public class SolvedQuizDto
	{
		public Guid QuizId { get; set; }
		public PlayerAnswerDto[] PlayerAnswers { get; set; }

		public SolvedQuizDto()
		{
		}

		public SolvedQuizDto(Guid quizId, PlayerAnswerDto[] playerAnswers)
		{
			QuizId = quizId;
			PlayerAnswers = playerAnswers;
		}
	}
}