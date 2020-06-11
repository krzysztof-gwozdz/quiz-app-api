using System;

namespace QuizApp.Application.Dtos
{
	public class QuizParametersDto
	{
		public Guid QuestionSetId { get; set; }
		public int QuestionCount { get; set; }

		public QuizParametersDto()
		{
		}

		public QuizParametersDto(Guid questionSetId, int questionCount)
		{
			QuestionSetId = questionSetId;
			QuestionCount = questionCount;
		}
	}
}
