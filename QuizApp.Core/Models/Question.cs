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

		public Question(Guid id, string text, ISet<Answer> answers, ISet<string> tags)
		{
			Id = id;
			Text = text;
			Answers = answers;
			Tags = tags;
		}

		public Question(string text, ISet<Answer> answers, ISet<string> tags)
			: this(Guid.NewGuid(), text, answers, tags)
		{
		}

		public static Question Create(string text, ISet<Answer> answers, ISet<string> tags)
		{
			if (string.IsNullOrWhiteSpace(text))
				throw new EmptyQuestionTextException();

			//TODO What if answers are null?
			if (answers.Count < MinNumberOfAnswers)
				throw new InvalidNumberOfAnswersInQuestionException(answers.Count);

			var duplicates = answers.GroupBy(x => x.Text).SelectMany(d => d.Skip(1));
			if (duplicates.Any())
				throw new QuestionContainsDuplicatedAnswersException(duplicates.First().Text);

			var correctAnswerCount = answers.Count(answer => answer.IsCorrect);
			if (correctAnswerCount != 1)
				throw new NotExactlyOneAnswerIsCorrectException(correctAnswerCount);

			//TODO What if tags are null?
			if (!tags.Any())
				throw new EmptyQuestionTagsException();

			return new Question(text, answers.ToHashSet(), tags);
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
