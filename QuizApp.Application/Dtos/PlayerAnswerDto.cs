using System;

namespace QuizApp.Application.Dtos
{
	public class PlayerAnswerDto
	{
		public Guid QuestionId { get; set; }
		public Guid AnswerId { get; set; }

		public PlayerAnswerDto()
		{
		}

		public PlayerAnswerDto(Guid questionId, Guid answerId)
		{
			QuestionId = questionId;
			AnswerId = answerId;
		}
	}
}