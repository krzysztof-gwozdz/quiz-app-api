using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionSetDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Color { get; set; }
		public int TotalQuestions { get; set; }

		public string ImageUrl { get; set; }

		public QuestionSetDto()
		{
		}

		public QuestionSetDto(Guid id, string name, string description, string color, int totalQuestions)
		{
			Id = id;
			Name = name;
			Description = description;
			Color = color;
			TotalQuestions = totalQuestions;
		}
	}
}
