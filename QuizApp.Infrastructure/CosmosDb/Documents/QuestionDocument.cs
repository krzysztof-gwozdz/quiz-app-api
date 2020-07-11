using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class QuestionDocument
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("answers")]
		public ISet<AnswerDocument> Answers { get; set; }

		[JsonProperty("tags")]
		public string[] Tags { get; set; }

		[JsonProperty("correctAnswersCount")]
		public int CorrectAnswersCount { get; set; }

		[JsonProperty("allAnswersCount")]
		public int AllAnswersCount { get; set; }

		public class AnswerDocument
		{
			[JsonProperty("id")]
			public Guid Id { get; set; }

			[JsonProperty("text")]
			public string Text { get; set; }

			[JsonProperty("isCorrect")]
			public bool IsCorrect { get; set; }
		}
	}
}
