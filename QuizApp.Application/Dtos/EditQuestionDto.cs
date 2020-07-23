using System;

namespace QuizApp.Application.Dtos
{
	public class EditQuestionDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public EditAnswerDto[] Answers { get; set; }
		public string[] Tags { get; set; }

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
