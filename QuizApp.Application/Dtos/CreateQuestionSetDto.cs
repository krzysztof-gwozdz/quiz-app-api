using Microsoft.AspNetCore.Http;

namespace QuizApp.Application.Dtos
{
	public class CreateQuestionSetDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string[] Tags { get; set; }
		public IFormFile Image { get; set; }
		public string Color { get; set; }

		public CreateQuestionSetDto()
		{
		}

		public CreateQuestionSetDto(string name, string description, string[] tags, IFormFile image, string color)
		{
			Name = name;
			Description = description;
			Tags = tags;
			Image = image;
			Color = color;
		}
	}
}
