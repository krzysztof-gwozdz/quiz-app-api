using CsvHelper.Configuration.Attributes;

namespace QuizApp.Importer.GoogleSheets.Questions
{
	internal class QuestionRow
	{
		[Index(0)]
		public string IsComplete { get; set; }

		[Index(1)]
		public string IsValid { get; set; }

		[Index(2)]
		public string Tag1 { get; set; }

		[Index(3)]
		public string Tag2 { get; set; }

		[Index(4)]
		public string Tag3 { get; set; }

		[Index(5)]
		public string Question { get; set; }

		[Index(6)]
		public string Answer1 { get; set; }

		[Index(7)]
		public string Answer2 { get; set; }

		[Index(8)]
		public string Answer3 { get; set; }

		[Index(9)]
		public string Answer4 { get; set; }
	}
}
