namespace QuizApp.Application.Dtos
{
	public class TagDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int TotalQuestions { get; set; }

		public TagDto()
		{
		}

		public TagDto(string name, string description, int totalQuestions)
		{
			Name = name;
			Description = description;
			TotalQuestions = totalQuestions;
		}
	}
}
