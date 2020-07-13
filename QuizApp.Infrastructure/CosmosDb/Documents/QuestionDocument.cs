using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class QuestionDocument
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public ISet<AnswerDocument> Answers { get; set; }
		public string[] Tags { get; set; }
		public int CorrectAnswersCount { get; set; }
		public int AllAnswersCount { get; set; }

		public QuestionDocument(Guid id, string text, ISet<AnswerDocument> answers, string[] tags, int correctAnswersCount, int allAnswersCount)
		{
			Id = id;
			Text = text;
			Answers = answers;
			Tags = tags;
			CorrectAnswersCount = correctAnswersCount;
			AllAnswersCount = allAnswersCount;
		}

		public class AnswerDocument
		{
			public Guid Id { get; set; }
			public string Text { get; set; }
			public bool IsCorrect { get; set; }

			public AnswerDocument(Guid id, string text, bool isCorrect)
			{
				Id = id;
				Text = text;
				IsCorrect = isCorrect;
			}
		}
	}
}
