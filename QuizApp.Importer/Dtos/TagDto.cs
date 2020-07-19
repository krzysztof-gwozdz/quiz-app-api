namespace QuizApp.Importer.Dtos
{
	internal class TagDto
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public TagDto(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public override string ToString()
			=> $"{Name} ({Description})";
	}
}
