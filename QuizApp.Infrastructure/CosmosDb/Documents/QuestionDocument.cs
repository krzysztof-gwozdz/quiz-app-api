using Newtonsoft.Json;
using QuizApp.Core.Models;
using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	internal class QuestionDocument
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("answers")]
		public ISet<AnswerDocument> Answers { get; set; }

		[JsonProperty("tags")]
		public string[] Tags { get; set; }

		[JsonProperty("Status")]
		public Question.Statuses Status { get; set; }

		[JsonProperty("correctAnswersCount")]
		public int CorrectAnswersCount { get; set; }

		[JsonProperty("allAnswersCount")]
		public int AllAnswersCount { get; set; }

		public QuestionDocument(Guid id, string text, ISet<AnswerDocument> answers, string[] tags, Question.Statuses status, int correctAnswersCount, int allAnswersCount)
		{
			Id = id;
			Text = text;
			Answers = answers;
			Tags = tags;
			Status = status;
			CorrectAnswersCount = correctAnswersCount;
			AllAnswersCount = allAnswersCount;
		}

		public class AnswerDocument
		{
			[JsonProperty("id")]
			public Guid Id { get; set; }

			[JsonProperty("text")]
			public string Text { get; set; }

			[JsonProperty("isCorrect")]
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
