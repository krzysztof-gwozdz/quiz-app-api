using System;

namespace QuizApp.Api.Dtos
{
	public class QuizParameters
	{
		public Guid QuestionSetId { get; set; }
		public int QuestionCount { get; set; }
	}
}
