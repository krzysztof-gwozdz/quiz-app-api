using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSetEntity ToEntity(this QuestionSet model) =>
			new QuestionSetEntity { Id = model.Id, Name = model.Name, IconId = model.IconId, Color = model.Color.Value };

		public static QuestionSet ToModel(this QuestionSetEntity entity) =>
			new QuestionSet(entity.Id, entity.Name, entity.IconId, new Color(entity.Color));

		public static ISet<QuestionSet> ToModel(this ISet<QuestionSetEntity> entities) =>
			new HashSet<QuestionSet>(entities.Select(ToModel));
	}
}
