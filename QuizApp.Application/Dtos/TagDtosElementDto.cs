namespace QuizApp.Application.Dtos
{
	public class TagDtosElementDto
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public TagDtosElementDto()
		{
		}

		public TagDtosElementDto(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
