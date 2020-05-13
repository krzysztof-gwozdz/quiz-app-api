﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class Quiz
	{
		public Guid Id { get; }
		public Question[] Questions { get; }
		public int CorrectAnswers => Questions?.Count(question => question.IsCorrect) ?? 0;
		public int TotalQuestions => Questions?.Count() ?? 0;

		public Quiz(Guid id, Question[] questions)
		{
			Id = id;
			Questions = questions;
		}

		public static Quiz Create(Question[] questions)
		{
			return new Quiz(Guid.NewGuid(), questions);
		}

		public void Resolve(IEnumerable<PlayerAnswer> playerAnswers)
		{
			playerAnswers.ToList().ForEach(playerAnswer => Questions.First(x => x.Id == playerAnswer.QuestionId).AnswerQuestion(playerAnswer.AnswerId));
		}

		public class Question
		{
			public Guid Id { get; }
			public string Text { get; }
			public ISet<Answer> Answers { get; }
			public Guid CorrectAnswerId { get; }
			public Guid? PlayerAnswerId { get; private set; }
			public Guid QuestionSetId { get; }

			public bool IsCorrect => CorrectAnswerId == PlayerAnswerId;

			public Question(Guid id, string text, ISet<Answer> answers, Guid correctAnswerId, Guid? playerAnswerId, Guid questionSetId)
			{
				Id = id;
				Text = text;
				Answers = answers;
				CorrectAnswerId = correctAnswerId;
				PlayerAnswerId = playerAnswerId;
				QuestionSetId = questionSetId;
			}

			public Question(string text, ISet<Answer> answers, Guid correctAnswerId, Guid? playerAnswerId, Guid questionSetId)
				: this(Guid.NewGuid(), text, answers, correctAnswerId, playerAnswerId, questionSetId)
			{
			}

			public Question(Models.Question question)
				: this(question.Id, question.Text, question.Answers.Select(answer => new Answer(answer)).ToHashSet(), question.CorrectAnswerId, null, question.QuestionSetId)
			{
			}

			public static Question Create(string text, ISet<Answer> answers, Guid correctAnswerId, Guid? playerAnswerId, Guid questionSetId)
			{
				return new Question(text, answers.ToHashSet(), correctAnswerId, playerAnswerId, questionSetId);
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

				private Answer(string text)
					: this(Guid.NewGuid(), text)
				{
				}

				public static Answer Create(string text)
				{
					return new Answer(text);
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
