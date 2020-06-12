using System;

namespace QuizApp.Application.Dtos
{
	public class PlayerAnswerDto
	{
		public Guid QuestionId { get; set; }
		public Guid AnswerId { get; set; }
		public int? Rating { get; set; }

		public PlayerAnswerDto()
		{
		}

		public PlayerAnswerDto(Guid questionId, Guid answerId, int? rating)
		{
			QuestionId = questionId;
			AnswerId = answerId;
			Rating = rating;
		}
	}
}