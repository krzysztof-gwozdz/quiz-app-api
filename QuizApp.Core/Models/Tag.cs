using QuizApp.Core.Exceptions;
using System;

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
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyTagNameException();

			//TODO VALIDATION EXCPETIONS + Problem Details
			if (string.IsNullOrWhiteSpace(description))
				throw new EmptyTagDescriptionException();

			return new Tag(Guid.NewGuid(), name, description);
		}
	}
}
