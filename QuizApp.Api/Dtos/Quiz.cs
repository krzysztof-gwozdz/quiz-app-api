using System;

namespace QuizApp.Api.Dtos
{
	public class Quiz
	{
		public Guid Id { get; set; }
		public Question[] Questions { get; set; }
	}
}
