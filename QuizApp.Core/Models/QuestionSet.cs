using QuizApp.Core.Exceptions;
using System;

namespace QuizApp.Core.Models
{
	public class QuestionSet
	{
		public Guid Id { get; }
		public string Name { get; }
		public string Description { get; }
		public Guid ImageId { get; }
		public Color Color { get; }

		public QuestionSet(Guid id, string name, string description, Guid imageId, Color color)
		{
			Id = id;
			Name = name;
			Description = description;
			ImageId = imageId;
			Color = color;
		}

		private QuestionSet(string name, string description, Guid imageId, Color color)
			: this(Guid.NewGuid(), name, description, imageId, color)
		{
		}

		public static QuestionSet Create(string name, string description, Guid imageId, Color color)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyQuestionSetNameException();

			if (string.IsNullOrWhiteSpace(description))
				throw new EmptyQuestionSetDescriptionException();

			if (Guid.Empty == imageId)
				throw new EmptyQuestionSetImageException();

			return new QuestionSet(name, description, imageId, color);
		}
	}
}
