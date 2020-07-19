namespace QuizApp.Importer.GoogleSheets.Tags
{
	internal class TagRow
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public TagRow(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
