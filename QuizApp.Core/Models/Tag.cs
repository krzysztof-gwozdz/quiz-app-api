using QuizApp.Core.Exceptions;

namespace QuizApp.Core.Models
{
	public class Tag
	{
		public string Name { get; }
		public string Description { get; }

		public Tag(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public static Tag Create(string name, string description)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EmptyTagNameException();

			if (string.IsNullOrWhiteSpace(description))
				throw new EmptyTagDescriptionException();

			return new Tag(name, description);
		}
	}
}
