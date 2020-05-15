using QuizApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class Question
	{
		private const int MinNumberOfAnswers = 2;

		public Guid Id { get; }
		public string Text { get; }
		public ISet<Answer> Answers { get; }
		public Guid CorrectAnswerId { get; }
		public Guid QuestionSetId { get; }

		public Question(Guid id, string text, ISet<Answer> answers, Guid correctAnswerId, Guid questionSetId)
		{
			Id = id;
			Text = text;
			Answers = answers;
			CorrectAnswerId = correctAnswerId;
			QuestionSetId = questionSetId;
		}

		public Question(string text, ISet<Answer> answers, Guid correctAnswerId, Guid questionSetId)
			: this(Guid.NewGuid(), text, answers, correctAnswerId, questionSetId)
		{
		}

		public static Question Create(string text, ISet<Answer> answers, string correctAnswer, Guid questionSetId)
		{
			if (string.IsNullOrWhiteSpace(text))
				throw new EmptyQuestionTextException();

			if (answers.Count <= MinNumberOfAnswers)
				throw new InvalidNumberOfAnswersInQuestionException(answers.Count);

			var duplicates = answers.GroupBy(x => x.Text).SelectMany(d => d.Skip(1));
			if (duplicates.Any())
				throw new QuestionContainsDuplicatedAnswersException(duplicates.First().Text);

			if (string.IsNullOrWhiteSpace(correctAnswer))
				throw new EmptyCorrectAnswerException();

			var correctAnswerId = answers.FirstOrDefault(x => x.Text == correctAnswer)?.Id;
			if (correctAnswerId is null)
				throw new CorrectAnswerIsNotOneOfAnswersException(correctAnswer);

			return new Question(text, answers.ToHashSet(), correctAnswerId.Value, questionSetId);
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

			private Answer(string text)
				: this(Guid.NewGuid(), text)
			{
			}

			public static Answer Create(string text)
			{
				if (string.IsNullOrWhiteSpace(text))
					throw new EmptyAnswerTextException();

				return new Answer(text);
			}
		}
	}
}
