using Newtonsoft.Json;
using QuizApp.Core.Models;
using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.Entities
{
	public class QuizEntity
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("questions")]
		public ISet<QuestionEntity> Questions { get; set; }

		public class QuestionEntity
		{
			[JsonProperty("id")]
			public Guid Id { get; set; }

			[JsonProperty("text")]
			public string Text { get; set; }

			[JsonProperty("answers")]
			public ISet<AnswerEntity> Answers { get; set; }

			[JsonProperty("questionSetId")]
			public Guid QuestionSetId { get; set; }

			[JsonProperty("correctAnswerId")]
			public Guid CorrectAnswerId { get; set; }

			[JsonProperty("playerAnswerId")]
			public Guid? PlayerAnswerId { get; set; }

			[JsonProperty("playerRating")]
			public QuestionRatings? PlayerRating { get; set; }

			public class AnswerEntity
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
