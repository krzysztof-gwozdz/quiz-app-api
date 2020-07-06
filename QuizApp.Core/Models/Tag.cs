using QuizApp.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class Tag
	{
		public Guid Id { get; }
		public string Name { get; }
		public string Description { get; }

		public Tag(Guid id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}

		public static Tag Create(string name, string description)
		{
			Validate(name, description);
			return new Tag(Guid.NewGuid(), name, description);
		}

		public static void Validate(string name, string description)
		{
			var errors = new HashSet<ValidationError>();

			if (string.IsNullOrWhiteSpace(name))
				errors.Add(new ValidationError(nameof(name), "Tag name can not be empty."));
			if (string.IsNullOrWhiteSpace(description))
				errors.Add(new ValidationError(nameof(description), "Tag description can not be empty."));

			if (errors.Any())
				throw new ValidationException(errors.ToArray());
		}
	}
}
