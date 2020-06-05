using Microsoft.AspNetCore.Http;

namespace QuizApp.Application.Dtos
{
	public class CreateQuestionSetDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public IFormFile Image { get; set; }
		public string Color { get; set; }
	}
}
