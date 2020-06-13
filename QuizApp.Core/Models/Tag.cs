using QuizApp.Core.Exceptions;
using System;

namespace QuizApp.Core.Models
{
	public class Tag
	{
		public Guid Id { get; }
		public string Name { get; }

		public Tag(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		public static Tag Create(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyTagNameException();

			return new Tag(Guid.NewGuid(), name);
		}
	}
}
