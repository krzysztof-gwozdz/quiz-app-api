using QuizApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class Quiz
	{
		public Guid Id { get; }
		public Guid QuestionSetId { get; }
		public HashSet<Question> Questions { get; }
		public int CorrectAnswers => Questions?.Count(question => question.IsCorrect == true) ?? 0;
		public int TotalQuestions => Questions?.Count ?? 0;

		public Quiz(Guid id, Guid questionSetId, HashSet<Question> questions)
		{
			Id = id;
			QuestionSetId = questionSetId;
			Questions = questions;
		}

		public void Solve(HashSet<PlayerAnswer> playerAnswers)
		{
			foreach (var playerAnswer in playerAnswers)
				Solve(playerAnswer);
		}

		private void Solve(PlayerAnswer playerAnswer)
		{
			var question = TryGetQuestion(playerAnswer.QuestionId);
			CheckIfQuestionIsFromQuiz(playerAnswer, question.Answers);
			question.AnswerQuestion(playerAnswer.AnswerId);
			question.Rate(playerAnswer.Rating);
		}

		private Question TryGetQuestion(Guid questionId) =>
			Questions.FirstOrDefault(x => x.Id == questionId) ?? throw new QuestionIsNotAPartOfQuizException(questionId, Id);

		private void CheckIfQuestionIsFromQuiz(PlayerAnswer playerAnswer, ISet<Question.Answer> answers)
		{
			if (!answers.Any(x => x.Id == playerAnswer.AnswerId))
				throw new AnswerIsNotFromQuestionException(playerAnswer.AnswerId, playerAnswer.QuestionId);
		}

		public class Question
		{
			public Guid Id { get; }
			public string Text { get; }
			public ISet<Answer> Answers { get; }
			public ISet<string> Tags { get; }
			public Guid? PlayerAnswerId { get; private set; }
			public QuestionRatings? PlayerRating { get; private set; }

			public bool IsAnswered => PlayerAnswerId.HasValue;
			public Guid CorrectAnswerId => Answers.Single(answer => answer.IsCorrect).Id;
			public bool? IsCorrect => IsAnswered ? CorrectAnswerId == PlayerAnswerId : (bool?)null;

			public Question(Guid id, string text, ISet<Answer> answers, ISet<string> tags, Guid? playerAnswerId, QuestionRatings? playerRating)
			{
				Id = id;
				Text = text;
				Answers = answers;
				Tags = tags;
				PlayerAnswerId = playerAnswerId;
				PlayerRating = playerRating;
			}

			public Question(Models.Question question)
				: this(question.Id, question.Text, question.Answers.Select(answer => new Answer(answer)).ToHashSet(), question.Tags, null, null)
			{
			}

			public void AnswerQuestion(Guid answerId)
			{
				PlayerAnswerId = answerId;
			}

			public void Rate(QuestionRatings? rating)
			{
				PlayerRating = rating;
			}

			public class Answer
			{
				public Guid Id { get; }
				public string Text { get; }
				public bool IsCorrect { get; }

				public Answer(Guid id, string text, bool isCorrect)
				{
					Id = id;
					Text = text;
					IsCorrect = isCorrect;
				}

				public Answer(Models.Question.Answer answer)
					: this(answer.Id, answer.Text, answer.IsCorrect)
				{
				}
			}
		}

		public class PlayerAnswer
		{
			public Guid QuestionId { get; }
			public Guid AnswerId { get; }
			public QuestionRatings? Rating { get; }

			private PlayerAnswer(Guid questionId, Guid answerId, QuestionRatings? rating)
			{
				QuestionId = questionId;
				AnswerId = answerId;
				Rating = rating;
			}

			public static PlayerAnswer Create(Guid questionId, Guid answerId, int? rating)
			{
				if (rating.HasValue && !Enum.IsDefined(typeof(QuestionRatings), rating))
					throw new QuestionRatingIsOutOfRangeException(rating);

				return new PlayerAnswer(questionId, answerId, (QuestionRatings?)rating);
			}
		}
	}
}
