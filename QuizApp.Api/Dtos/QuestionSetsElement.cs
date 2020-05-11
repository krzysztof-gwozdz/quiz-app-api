using System;

namespace QuizApp.Api.Dtos
{
	public class QuestionSetsElement
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Color { get; set; }
	}
}
