using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionSetsElementDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public string Color { get; set; }

		private QuestionSetsElementDto()
		{
		}

		public QuestionSetsElementDto(Guid id, string name, string description, string imageUrl, string color)
		{
			Id = id;
			Name = name;
			Description = description;
			ImageUrl = imageUrl;
			Color = color;
		}
	}
}
