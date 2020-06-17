using Microsoft.AspNetCore.Http;
using System.Linq;

namespace QuizApp.Application.Dtos
{
	public class CreateQuestionSetDto
	{
		public string Name { get; set; }
		public string Description { get; set; }

		private string[] _tags;
		public string[] Tags
		{
			get => _tags;
			set => _tags = value?.First().Split(',');
		}

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
