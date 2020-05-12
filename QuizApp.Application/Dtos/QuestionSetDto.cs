using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionSetDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Color { get; set; }
		public int TotalQuestions { get; set; }
	}
}
