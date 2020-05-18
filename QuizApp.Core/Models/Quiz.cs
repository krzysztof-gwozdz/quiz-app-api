using QuizApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class Quiz
	{
		public Guid Id { get; }
		public HashSet<Question> Questions { get; }
		public int CorrectAnswers => Questions?.Count(question => question.IsCorrect == true) ?? 0;
		public int TotalQuestions => Questions?.Count() ?? 0;

		public Quiz(Guid id, HashSet<Question> questions)
		{
			Id = id;
			Questions = questions;
		}

		public void Solve(HashSet<PlayerAnswer> playerAnswers)
		{
			foreach (var playerAnswer in playerAnswers)
			{
				var question = Questions.FirstOrDefault(x => x.Id == playerAnswer.QuestionId);
				if (question is null)
					throw new QuestionIsNotAPartOfQuizException(playerAnswer.QuestionId, Id);

				if (!question.Answers.Any(x => x.Id == playerAnswer.AnswerId))
					throw new AnswerIsNotFromQuestionException(playerAnswer.AnswerId, playerAnswer.QuestionId);

				question.AnswerQuestion(playerAnswer.AnswerId);
			}
		}

		public class Question
		{
			public Guid Id { get; }
			public string Text { get; }
			public ISet<Answer> Answers { get; }
			public Guid CorrectAnswerId { get; }
			public Guid QuestionSetId { get; }
			public Guid? PlayerAnswerId { get; private set; }

			public bool IsAnswered => PlayerAnswerId.HasValue;
			public bool? IsCorrect => IsAnswered ? CorrectAnswerId == PlayerAnswerId : (bool?)null;

			public Question(Guid id, string text, ISet<Answer> answers, Guid correctAnswerId, Guid? playerAnswerId, Guid questionSetId)
			{
				Id = id;
				Text = text;
				Answers = answers;
				CorrectAnswerId = correctAnswerId;
				PlayerAnswerId = playerAnswerId;
				QuestionSetId = questionSetId;
			}

			public Question(Models.Question question)
				: this(question.Id, question.Text, question.Answers.Select(answer => new Answer(answer)).ToHashSet(), question.CorrectAnswerId, null, question.QuestionSetId)
			{
			}

			public void AnswerQuestion(Guid playerAnswerId)
			{
				PlayerAnswerId = playerAnswerId;
			}

			public class Answer
			{
				public Guid Id { get; }
				public string Text { get; }

				public Answer(Guid id, string text)
				{
					Id = id;
					Text = text;
				}

				public Answer(Models.Question.Answer answer)
					: this(answer.Id, answer.Text)
				{
				}
			}
		}

		public class PlayerAnswer
		{
			public Guid QuestionId { get; }
			public Guid AnswerId { get; }

			private PlayerAnswer(Guid questionId, Guid answerId)
			{
				QuestionId = questionId;
				AnswerId = answerId;
			}

			public static PlayerAnswer Create(Guid questionId, Guid answerId)
			{
				return new PlayerAnswer(questionId, answerId);
			}
		}
	}
}
