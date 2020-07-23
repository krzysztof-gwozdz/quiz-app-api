namespace QuizApp.Application.Dtos
{
	public class CreateQuestionDto
	{
		public string Text { get; set; }
		public CreateAnswerDto[] Answers { get; set; }
		public string[] Tags { get; set; }

		public CreateQuestionDto()
		{
		}

		public CreateQuestionDto(string text, CreateAnswerDto[] answers, string[] tags)
		{
			Text = text;
			Answers = answers;
			Tags = tags;
		}
	}
}
