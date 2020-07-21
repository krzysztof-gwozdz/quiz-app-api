using CsvHelper.Configuration.Attributes;

namespace QuizApp.Importer.GoogleSheets.Tags
{
	internal class TagRow
	{
		[Index(0)]
		public string Name { get; set; }

		[Index(1)]
		public string Description { get; set; }
	}
}
