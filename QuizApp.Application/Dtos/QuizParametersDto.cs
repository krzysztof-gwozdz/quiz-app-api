using System;

namespace QuizApp.Application.Dtos
{
	public class QuizParametersDto
	{
		public Guid QuestionSetId { get; set; }
		public int QuestionCount { get; set; }

		private QuizParametersDto()
		{
		}

		public QuizParametersDto(Guid questionSetId, int questionCount)
		{
			QuestionSetId = questionSetId;
			QuestionCount = questionCount;
		}
	}
}
