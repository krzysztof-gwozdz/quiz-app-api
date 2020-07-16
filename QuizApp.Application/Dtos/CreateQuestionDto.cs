using System.Linq;

namespace QuizApp.Application.Dtos
{
	public class CreateQuestionDto
	{
		public string Text { get; set; }
		public CreateAnswerDto[] Answers { get; set; }

		private string[] _tags;
		public string[] Tags
		{
			get => _tags;
			set => _tags = value?.First().Split(',');
		}

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
