using QuizApp.Shared.Exceptions;
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
			Validate(name, description, tags, imageId, color);
			return new QuestionSet(imageId, name, description, tags, imageId, color);
		}

		public static void Validate(string name, string description, ISet<Tag> tags, Guid imageId, Color color)
		{
			var errors = new HashSet<ValidationError>();

			if (string.IsNullOrWhiteSpace(name))
				errors.Add(new ValidationError(nameof(name), "Question set name can not be empty."));
			if (string.IsNullOrWhiteSpace(description))
				errors.Add(new ValidationError(nameof(description), "Question set description can not be empty."));
			if (tags is null || !tags.Any())
				errors.Add(new ValidationError(nameof(tags), "Question set tag collection can not be empty."));
			if (Guid.Empty == imageId)
				errors.Add(new ValidationError("image", "Question set image can not be empty."));
			if (color is null)
				errors.Add(new ValidationError(nameof(color), "Question set color can not be empty."));

			if (errors.Any())
				throw new ValidationException(errors.ToArray());
		}
	}
}
