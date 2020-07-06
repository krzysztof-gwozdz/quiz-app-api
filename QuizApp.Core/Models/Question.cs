using QuizApp.Shared.Exceptions;
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
			Validate(text, answers);
			return new Question(text, answers.ToHashSet(), questionSetId);
		}

		public static void Validate(string text, ISet<Answer> answers)
		{
			var errors = new HashSet<ValidationError>();

			if (string.IsNullOrWhiteSpace(text))
				errors.Add(new ValidationError(nameof(text), "Question text can not be empty."));
			if (answers is null || answers.Count < MinNumberOfAnswers)
				errors.Add(new ValidationError(nameof(answers), $"Number of answers in question invalid: {answers?.Count ?? 0}."));
			else
			{
				var duplicates = answers.GroupBy(x => x.Text).SelectMany(d => d.Skip(1));
				if (duplicates.Any())
					errors.Add(new ValidationError(nameof(answers), $"Question contains duplicated answers: {duplicates.First().Text}."));
				var correctAnswerCount = answers.Count(answer => answer.IsCorrect);
				if (correctAnswerCount != 1)
					errors.Add(new ValidationError(nameof(answers), $"Not exactly one answer is correct exception. Correct answer count: {correctAnswerCount}"));
			}

			if (errors.Any())
				throw new ValidationException(errors.ToArray());
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
				Validate(text);
				return new Answer(text, isCorrect);
			}

			public static void Validate(string text)
			{
				var errors = new HashSet<ValidationError>();

				if (string.IsNullOrWhiteSpace(text))
					errors.Add(new ValidationError(nameof(text), "Answer text can not be empty."));

				if (errors.Any())
					throw new ValidationException(errors.ToArray());
			}
		}
	}
}
