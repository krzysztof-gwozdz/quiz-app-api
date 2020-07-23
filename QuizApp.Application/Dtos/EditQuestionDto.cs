using System;
using System.Linq;

namespace QuizApp.Application.Dtos
{
	public class EditQuestionDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public EditAnswerDto[] Answers { get; set; }

		private string[] _tags;
		public string[] Tags
		{
			get => _tags;
			set => _tags = value?.First().Split(',');
		}

		public EditQuestionDto()
		{
		}

		public EditQuestionDto(Guid id, string text, EditAnswerDto[] answers, string[] tags)
		{
			Id = id;
			Text = text;
			Answers = answers;
			Tags = tags;
		}
	}
}
