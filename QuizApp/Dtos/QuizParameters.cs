using System;

namespace QuizApp.Dtos
{
	public class QuizParameters
	{
		public Guid QuestionSetId { get; set; }
		public int QuestionCount { get; set; }
	}
}
