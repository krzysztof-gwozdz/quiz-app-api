using Newtonsoft.Json;
using QuizApp.Core.Models;
using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class QuizDocument
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("questionSetId")]
		public Guid QuestionSetId { get; set; }

		[JsonProperty("questions")]
		public ISet<QuestionDocument> Questions { get; set; }

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

			[JsonProperty("correctAnswerId")]
			public Guid CorrectAnswerId { get; set; }

			[JsonProperty("playerAnswerId")]
			public Guid? PlayerAnswerId { get; set; }

			[JsonProperty("playerRating")]
			public QuestionRatings? PlayerRating { get; set; }

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
}
