using System;

namespace QuizApp.Core.Models
{
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