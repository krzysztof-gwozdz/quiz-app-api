using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSetEntity ToEntity(this QuestionSet model) =>
			new QuestionSetEntity
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				Tags = model.Tags.ToArray(),
				ImageId = model.ImageId,
				Color = model.Color.Value
			};

		public static QuestionSet ToModel(this QuestionSetEntity entity) =>
			new QuestionSet
			(
				entity.Id,
				entity.Name,
				entity.Description,
				entity.Tags.ToHashSet(),
				entity.ImageId,
				new Color(entity.Color)
			);

		public static ISet<QuestionSet> ToModel(this ISet<QuestionSetEntity> entities) =>
			new HashSet<QuestionSet>(entities.Select(ToModel));
	}
}
