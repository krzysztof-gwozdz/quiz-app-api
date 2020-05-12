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

		public Question(Guid id, string text, ISet<Answer> answers, Guid correctAnswerId)
		{
			Id = id;
			Text = text;
			Answers = answers;
			CorrectAnswerId = correctAnswerId;
		}

		public Question(string text, ISet<Answer> answers, Guid correctAnswerId)
			: this(Guid.NewGuid(), text, answers, correctAnswerId)
		{
		}

		public static Question Create(string text, ISet<Answer> answers, string correctAnswer)
		{
			var correctAnswerId = answers.First(x => x.Text == correctAnswer).Id;
			return new Question(text, answers.ToHashSet(), correctAnswerId);
		}
	}
}
