using System;

namespace QuizApp.Application.Dtos
{
	public class PlayerAnswerDto
	{
		public Guid QuestionId { get; set; }
		public Guid AnswerId { get; set; }
	}
}