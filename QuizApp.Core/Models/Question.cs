using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class Question
	{
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
			var correctAnswerId = answers.First(x => x.Text == correctAnswer).Id;
			return new Question(text, answers.ToHashSet(), correctAnswerId, questionSetId);
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
				return new Answer(text);
			}
		}
	}
}
