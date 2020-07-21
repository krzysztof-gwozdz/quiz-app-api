using QuizApp.Importer.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Importer.GoogleSheets.Questions
{
	internal class QuestionImporter : GoogleSheetsImporter<QuestionRow, QuestionDto>
	{
		public QuestionImporter(ILogger logger) : base(logger)
		{
		}

		protected override QuestionRow[] Filter(QuestionRow[] rows) => rows.Where(row => row.IsComplete == "✔️" && row.IsValid == "yes").ToArray();

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
	}
}