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
		public ISet<string> Tags { get; }
		public int CorrectAnswersCount { get; }
		public int AllAnswersCount { get; }
		public double RatioOfCorrectAnswers => AllAnswersCount > 0 ? (double)CorrectAnswersCount / AllAnswersCount : 1;

		public Question(Guid id, string text, ISet<Answer> answers, ISet<string> tags, int correctAnswersCount, int allAnswersCount)
		{
			Id = id;
			Text = text;
			Answers = answers;
			Tags = tags;
			CorrectAnswersCount = correctAnswersCount;
			AllAnswersCount = allAnswersCount;
		}

		public Question(string text, ISet<Answer> answers, ISet<string> tags, int correctAnswersCount, int allAnswersCount)
			: this(Guid.NewGuid(), text, answers, tags, correctAnswersCount, allAnswersCount)
		{
		}

		public static Question Create(string text, ISet<Answer> answers, ISet<string> tags)
		{
			if (string.IsNullOrWhiteSpace(text))
				throw new EmptyQuestionTextException();

			if (answers is null || answers.Count < MinNumberOfAnswers)
				throw new InvalidNumberOfAnswersInQuestionException(answers?.Count ?? 0);

			var duplicates = answers.GroupBy(x => x.Text).SelectMany(d => d.Skip(1));
			if (duplicates.Any())
				throw new QuestionContainsDuplicatedAnswersException(duplicates.First().Text);

			var correctAnswerCount = answers.Count(answer => answer.IsCorrect);
			if (correctAnswerCount != 1)
				throw new NotExactlyOneAnswerIsCorrectException(correctAnswerCount);

			if (tags is null || !tags.Any())
				throw new EmptyQuestionTagsException();

			return new Question(text, answers.ToHashSet(), tags, 0, 0);
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

			private Answer(string text, bool isCorrect)
				: this(Guid.NewGuid(), text, isCorrect)
			{
			}

			public static Answer Create(string text, bool isCorrect)
			{
				if (string.IsNullOrWhiteSpace(text))
					throw new EmptyAnswerTextException();

				return new Answer(text, isCorrect);
			}
		}
	}
}
