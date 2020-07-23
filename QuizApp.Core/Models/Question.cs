using QuizApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class Question
	{
		public enum Statuses { New = 0 }

		private const int MinNumberOfAnswers = 2;

		public Guid Id { get; }
		public string Text { get; set; }
		public ISet<Answer> Answers { get; set; }
		public ISet<string> Tags { get; set; }
		public Statuses Status { get; set; }
		public int CorrectAnswersCount { get; }
		public int AllAnswersCount { get; }
		public double RatioOfCorrectAnswers => AllAnswersCount > 0 ? (double)CorrectAnswersCount / AllAnswersCount : 1;

		public Question(Guid id, string text, ISet<Answer> answers, ISet<string> tags, Statuses status, int correctAnswersCount, int allAnswersCount)
		{
			Id = id;
			Text = text;
			Answers = answers;
			Tags = tags;
			Status = status;
			CorrectAnswersCount = correctAnswersCount;
			AllAnswersCount = allAnswersCount;
		}

		public Question(string text, ISet<Answer> answers, ISet<string> tags, Statuses status, int correctAnswersCount, int allAnswersCount)
			: this(Guid.NewGuid(), text, answers, tags, status, correctAnswersCount, allAnswersCount)
		{
		}

		public static Question Create(string text, ISet<Answer> answers, ISet<string> tags)
		{
			Validate(text, answers, tags);
			return new Question(text, answers.ToHashSet(), tags, Statuses.New, 0, 0);
		}

		public void Edit(string text, ISet<Answer> answers, ISet<string> tags)
		{
			Validate(text, answers, tags);
			Text = text;
			UpdateAnswers(answers);
			Tags = tags;
		}

		private static void Validate(string text, ISet<Answer> answers, ISet<string> tags)
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
		}

		private void UpdateAnswers(ISet<Answer> newAnswers)
		{
			foreach (var answer in Answers.ToArray())
			{
				var newAnswer = newAnswers.FirstOrDefault(x => x.Id == answer.Id);
				if (newAnswer is null)
					Answers.Remove(answer);
				else
				{
					answer.Edit(newAnswer.Text, newAnswer.IsCorrect);
					newAnswers.Remove(newAnswer);
				}
			}
			foreach (var newAnswer in newAnswers)
			{
				Answers.Add(newAnswer);
			}
		}

		public class Answer
		{
			public Guid Id { get; }
			public string Text { get; set; }
			public bool IsCorrect { get; set; }

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

			public void Edit(string text, bool isCorrect)
			{
				Validate(text);
				Text = text;
				IsCorrect = isCorrect;
			}

			private static void Validate(string text)
			{
				if (string.IsNullOrWhiteSpace(text))
					throw new EmptyAnswerTextException();
			}
		}
	}
}
