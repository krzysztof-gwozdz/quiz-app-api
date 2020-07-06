using System;

namespace QuizApp.Application.Dtos
{
	public class TagDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public TagDto()
		{
		}

		public TagDto(Guid id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
