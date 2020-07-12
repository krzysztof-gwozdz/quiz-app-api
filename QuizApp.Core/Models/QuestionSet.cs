using QuizApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class QuestionSet
	{
		public Guid Id { get; }
		public string Name { get; }
		public ISet<string> Tags { get; }
		public string Description { get; }
		public Guid ImageId { get; }
		public Color Color { get; }

		public QuestionSet(Guid id, string name, ISet<string> tags, string description, Guid imageId, Color color)
		{
			Id = id;
			Name = name;
			Tags = tags;
			Description = description;
			ImageId = imageId;
			Color = color;
		}

		public static QuestionSet Create(string name, ISet<string> tags, string description, Guid imageId, Color color)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyQuestionSetNameException();

			if (tags is null || !tags.Any())
				throw new EmptyQuestionSetsTagsException();

			if (string.IsNullOrWhiteSpace(description))
				throw new EmptyQuestionSetDescriptionException();

			if (Guid.Empty == imageId)
				throw new EmptyQuestionSetImageException();

			return new QuestionSet(imageId, name, tags, description, imageId, color);
		}
	}
}
