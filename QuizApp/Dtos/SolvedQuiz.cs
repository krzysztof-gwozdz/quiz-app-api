using System;

namespace QuizApp.Dtos
{
	public class SolvedQuiz
	{
		public Guid QuizId { get; set; }
		public PlayerAnswer[] PlayerAnswers { get; set; }
	}
}