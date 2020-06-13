using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.Mappers
{
	public static class TagsMapper
	{
		public static TagEntity ToEntity(this Tag model) =>
			new TagEntity { Id = model.Id, Name = model.Name };

		public static Tag ToModel(this TagEntity entity) =>
			new Tag(entity.Id, entity.Name);

		public static ISet<Tag> ToModel(this ISet<TagEntity> entities) =>
			new HashSet<Tag>(entities.Select(ToModel));
	}
}
