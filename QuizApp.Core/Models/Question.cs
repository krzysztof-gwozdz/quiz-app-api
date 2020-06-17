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
		public Guid QuestionSetId { get; }

		public Question(Guid id, string text, ISet<Answer> answers, Guid questionSetId)
		{
			Id = id;
			Text = text;
			Answers = answers;
			QuestionSetId = questionSetId;
		}

		public Question(string text, ISet<Answer> answers, Guid questionSetId)
			: this(Guid.NewGuid(), text, answers, questionSetId)
		{
		}

		public static Question Create(string text, ISet<Answer> answers, Guid questionSetId)
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

			return new Question(text, answers.ToHashSet(), questionSetId);
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
