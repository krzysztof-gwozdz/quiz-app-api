using QuizApp.Core.Exceptions;
using System;

namespace QuizApp.Core.Models
{
	public class QuestionSet
	{
		public Guid Id { get; }
		public string Name { get; }
		public string IconUrl { get; }
		public Color Color { get; }

		public QuestionSet(Guid id, string name, string iconUrl, Color color)
		{
			Id = id;
			Name = name;
			IconUrl = iconUrl;
			Color = color;
		}

		private QuestionSet(string name, string iconUrl, Color color)
			: this(Guid.NewGuid(), name, iconUrl, color)
		{
		}

		public static QuestionSet Create(string name, string iconUrl, Color color)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyQuestionSetNameException();

			return new QuestionSet(name, iconUrl, color);
		}
	}
}
