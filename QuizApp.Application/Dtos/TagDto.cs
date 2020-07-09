namespace QuizApp.Application.Dtos
{
	public class TagDto
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public TagDto()
		{
		}

		public TagDto(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
