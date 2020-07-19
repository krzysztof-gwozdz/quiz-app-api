namespace QuizApp.Importer.Dtos
{
	internal class QuestionDto
	{
		public string Text { get; set; }
		public AnswerDto[] Answers { get; set; }
		public string[] Tags { get; set; }

		public QuestionDto(string text, AnswerDto[] answers, string[] tags)
		{
			Text = text;
			Answers = answers;
			Tags = tags;
		}

		internal class AnswerDto
		{
			public string Text { get; set; }
			public bool IsCorrect { get; set; }

			public AnswerDto(string text, bool isCorrect)
			{
				Text = text;
				IsCorrect = isCorrect;
			}

			public override string ToString()
			{
				return $"{Text}({(IsCorrect ? 'T' : 'F')})";
			}
		}

		public override string ToString() => $"{Text} (\"{string.Join<AnswerDto>("\",\"", Answers)}\") [\"{string.Join<string>("\",\"", Tags)}\"]";
	}
}
