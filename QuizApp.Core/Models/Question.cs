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

		public Question(Guid id)
		{
			Id = id;
		}

		public static Question Create(string text, ISet<Answer> answers, string correctAnswer, Guid questionSetId)
		{
			var correctAnswerId = answers.First(x => x.Text == correctAnswer).Id;
			return new Question(text, answers.ToHashSet(), correctAnswerId, questionSetId);
		}
	}
}
