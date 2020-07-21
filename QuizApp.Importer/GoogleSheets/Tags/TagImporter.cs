using QuizApp.Importer.Dtos;
using System.Linq;

namespace QuizApp.Importer.GoogleSheets.Tags
{
	internal class TagImporter : GoogleSheetsImporter<TagRow, TagDto>
	{
		public TagImporter(ILogger logger) : base(logger)
		{
		}

		protected override TagDto[] MapResults(TagRow[] rows)
			=> rows.Select(row => new TagDto(row.Name, row.Description)).ToArray();
	}
}
