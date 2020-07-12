using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionsElementDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public int AnswersCount { get; set; }
		public string[] Tags { get; set; }
		public int CorrectAnswersCount { get; set; }
		public int AllAnswersCount { get; set; }
		public double RatioOfCorrectAnswers { get; set; }

		public QuestionsElementDto()
		{
		}

		public QuestionsElementDto(Guid id, string text, int answersCount, string[] tags, int correctAnswersCount, int allAnswersCount, double ratioOfCorrectAnswers)
		{
			Id = id;
			Text = text;
			AnswersCount = answersCount;
			Tags = tags;
			CorrectAnswersCount = correctAnswersCount;
			AllAnswersCount = allAnswersCount;
			RatioOfCorrectAnswers = ratioOfCorrectAnswers;
		}
	}
}
