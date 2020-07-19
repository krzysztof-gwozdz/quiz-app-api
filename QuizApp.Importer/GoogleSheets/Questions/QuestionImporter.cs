using QuizApp.Importer.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizApp.Importer.GoogleSheets.Questions
{
	internal class QuestionImporter : GoogleSheetsImporter<QuestionRow, QuestionDto>
	{
		public QuestionImporter(ILogger logger) : base(logger)
		{
		}

		protected override QuestionRow[] GetRows(Stream googleSheet)
		{
			using (var reader = new StreamReader(googleSheet))
			{
				var questionRows = new List<QuestionRow>();
				ReadHeaders(reader);
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();

					var values = line.Split(',');
					var isCompleteCell = values[(int)QuestionColumns.IsComplete];
					var isComplete = isCompleteCell == "✔️";
					var isValidCell = values[(int)QuestionColumns.IsValid];
					var isValid = isValidCell == "yes";

					questionRows.Add(new QuestionRow(
						isComplete,
						isValid,
						values[(int)QuestionColumns.Tag1],
						values[(int)QuestionColumns.Tag2],
						values[(int)QuestionColumns.Tag3],
						values[(int)QuestionColumns.Question],
						values[(int)QuestionColumns.Answer1],
						values[(int)QuestionColumns.Answer2],
						values[(int)QuestionColumns.Answer3],
						values[(int)QuestionColumns.Answer4]
						));
				}
				return questionRows.ToArray();
			}
		}

		protected override QuestionRow[] Filter(QuestionRow[] rows)
		{
			return rows.Where(row => row.IsComplete && row.IsValid).ToArray();
		}

		protected override QuestionDto[] MapResults(QuestionRow[] rows)
		{
			var questions = new List<QuestionDto>();
			foreach (var row in rows)
			{
				var text = row.Question;
				var answers = new QuestionDto.AnswerDto[]
				{
					new QuestionDto.AnswerDto(row.Answer1, true),
					new QuestionDto.AnswerDto(row.Answer2, false),
					new QuestionDto.AnswerDto(row.Answer3, false),
					new QuestionDto.AnswerDto(row.Answer4, false),
				};
				var tags = new List<string>();
				if (!string.IsNullOrWhiteSpace(row.Tag1)) tags.Add(row.Tag1);
				if (!string.IsNullOrWhiteSpace(row.Tag2)) tags.Add(row.Tag2);
				if (!string.IsNullOrWhiteSpace(row.Tag3)) tags.Add(row.Tag3);
				questions.Add(new QuestionDto(text, answers, tags.ToArray()));
			}
			return questions.ToArray();
		}
		private string ReadHeaders(StreamReader reader)
			=> reader.ReadLine();
	}
}