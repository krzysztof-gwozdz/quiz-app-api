using System;

namespace QuizApp.Application.Dtos
{
	public class TagDtosElementDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public TagDtosElementDto()
		{
		}

		public TagDtosElementDto(Guid id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
