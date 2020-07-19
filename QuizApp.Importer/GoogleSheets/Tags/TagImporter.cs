using QuizApp.Importer.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizApp.Importer.GoogleSheets.Tags
{
	internal class TagImporter : GoogleSheetsImporter<TagRow, TagDto>
	{
		public TagImporter(ILogger logger) : base(logger)
		{
		}

		protected override TagRow[] GetRows(Stream googleSheet)
		{
			using (var reader = new StreamReader(googleSheet))
			{
				var tagRows = new List<TagRow>();
				ReadHeaders(reader);
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');
					tagRows.Add(new TagRow(
						values[(int)TagColumns.Name],
						values[(int)TagColumns.Description]
						));
				}
				return tagRows.ToArray();
			}
		}

		protected override TagDto[] MapResults(TagRow[] rows)
			=> rows.Select(row => new TagDto(row.Name, row.Description)).ToArray();

		private string ReadHeaders(StreamReader reader)
			=> reader.ReadLine();
	}
}
