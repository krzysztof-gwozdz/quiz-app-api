using System;

namespace QuizApp.Application.Dtos
{
	public class TagDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public TagDto()
		{
		}

		public TagDto(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
