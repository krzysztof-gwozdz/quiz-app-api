namespace QuizApp.Application.Dtos
{
	public class CreateTagDto
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public CreateTagDto()
		{
		}

		public CreateTagDto(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
