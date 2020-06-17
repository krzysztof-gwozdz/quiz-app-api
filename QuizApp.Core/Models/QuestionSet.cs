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
		public string Description { get; }
		public ISet<Tag> Tags { get; }
		public Guid ImageId { get; }
		public Color Color { get; }

		public QuestionSet(Guid id, string name, string description, ISet<Tag> tags, Guid imageId, Color color)
		{
			Id = id;
			Name = name;
			Description = description;
			Tags = tags;
			ImageId = imageId;
			Color = color;
		}

		public static QuestionSet Create(string name, string description, ISet<Tag> tags, Guid imageId, Color color)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyQuestionSetNameException();

			if (string.IsNullOrWhiteSpace(description))
				throw new EmptyQuestionSetDescriptionException();

			//TODO What if tags are null?
			if (!tags.Any())
				throw new EmptyQuestionSetsTagsException();

			if (Guid.Empty == imageId)
				throw new EmptyQuestionSetImageException();

			return new QuestionSet(imageId, name, description, tags, imageId, color);
		}
	}
}
