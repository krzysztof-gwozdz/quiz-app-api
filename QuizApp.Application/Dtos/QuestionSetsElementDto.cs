﻿using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionSetsElementDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string[] Tags { get; set; }
		public string Color { get; set; }

		public string ImageUrl { get; set; }

		public QuestionSetsElementDto()
		{
		}

		public QuestionSetsElementDto(Guid id, string name, string description, string[] tags, string color)
		{
			Id = id;
			Name = name;
			Description = description;
			Tags = tags;
			Color = color;
		}
	}
}
