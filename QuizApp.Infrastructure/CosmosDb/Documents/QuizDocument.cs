using QuizApp.Core.Models;
using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class QuizDocument
	{
		public Guid Id { get; set; }
		public Guid QuestionSetId { get; set; }
		public ISet<QuestionDocument> Questions { get; set; }

		public QuizDocument(Guid id, Guid questionSetId, ISet<QuestionDocument> questions)
		{
			Id = id;
			QuestionSetId = questionSetId;
			Questions = questions;
		}

		public class QuestionDocument
		{
			public Guid Id { get; set; }
			public string Text { get; set; }
			public ISet<AnswerDocument> Answers { get; set; }
			public string[] Tags { get; set; }
			public Guid CorrectAnswerId { get; set; }
			public Guid? PlayerAnswerId { get; set; }
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
}
