using System;

namespace QuizApp.Dtos
{
	public class QuestionSet
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Color { get; set; }
		public int TotalQuestions { get; set; }
	}
}
