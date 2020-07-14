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

		public QuizDocument(Guid id, Guid questionSetId, ISet<QuestionDocument> questions)
		{
			Id = id;
			QuestionSetId = questionSetId;
			Questions = questions;
		}

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

			public QuestionDocument(Guid id, string text, ISet<AnswerDocument> answers, string[] tags, Guid correctAnswerId, Guid? playerAnswerId, QuestionRatings? playerRating)
			{
				Id = id;
				Text = text;
				Answers = answers;
				Tags = tags;
				CorrectAnswerId = correctAnswerId;
				PlayerAnswerId = playerAnswerId;
				PlayerRating = playerRating;
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
}
