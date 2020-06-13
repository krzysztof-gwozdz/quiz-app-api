namespace QuizApp.Application.Dtos
{
	public class CreateTagDto
	{
		public string Name { get; set; }

		public CreateTagDto()
		{
		}

		public CreateTagDto(string name)
		{
			Name = name;
		}
	}
}
