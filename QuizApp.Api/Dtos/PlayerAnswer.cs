using System;

namespace QuizApp.Api.Dtos
{
	public class PlayerAnswer
	{
		public Guid QuestionId { get; set; }
		public Guid AnswerId { get; set; }
	}
}