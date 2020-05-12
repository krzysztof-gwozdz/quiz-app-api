﻿using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;

namespace QuizApp.Infrastructure.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSetEntity ToEntity(this QuestionSet model) =>
			new QuestionSetEntity { Id = model.Id, Name = model.Name, IconUrl = model.IconUrl, Color = model.Color };

		public static QuestionSet FromEntity(this QuestionSetEntity entity) =>
			new QuestionSet(entity.Id, entity.Name, entity.IconUrl, entity.Color);
	}
}
