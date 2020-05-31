using QuizApp.Core.Exceptions;
using System;

namespace QuizApp.Core.Models
{
	public class QuestionSet
	{
		public Guid Id { get; }
		public string Name { get; }
		public Guid IconId { get; }
		public Color Color { get; }

		public QuestionSet(Guid id, string name, Guid iconId, Color color)
		{
			Id = id;
			Name = name;
			IconId = iconId;
			Color = color;
		}

		private QuestionSet(string name, Guid iconId, Color color)
			: this(Guid.NewGuid(), name, iconId, color)
		{
		}

		public static QuestionSet Create(string name, Guid iconId, Color color)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyQuestionSetNameException();

			if (Guid.Empty == iconId)
				throw new EmptyQuestionSetIconException();

			return new QuestionSet(name, iconId, color);
		}
	}
}
