using System;

namespace QuizApp.Infrastructure.Entities
{
	public class QuestionSetEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Color { get; set; }
	}
}
