using QuizApp.Core.Exceptions;
using System;

namespace QuizApp.Core.Models
{
	public class QuestionSet
	{
		public Guid Id { get; }
		public string Name { get; }
		public string Description { get; }
		public Guid IconId { get; }
		public Color Color { get; }

		public QuestionSet(Guid id, string name, string description, Guid iconId, Color color)
		{
			Id = id;
			Name = name;
			Description = description;
			IconId = iconId;
			Color = color;
		}

		private QuestionSet(string name, string description, Guid iconId, Color color)
			: this(Guid.NewGuid(), name, description, iconId, color)
		{
		}

		public static QuestionSet Create(string name, string description, Guid iconId, Color color)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyQuestionSetNameException();

			if (string.IsNullOrWhiteSpace(description))
				throw new EmptyQuestionSetDescriptionException();

			if (Guid.Empty == iconId)
				throw new EmptyQuestionSetIconException();

			return new QuestionSet(name, description, iconId, color);
		}
	}
}
