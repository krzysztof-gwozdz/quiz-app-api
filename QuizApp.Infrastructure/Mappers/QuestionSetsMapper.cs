using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;

namespace QuizApp.Infrastructure.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSetEntity ToEntity(this QuestionSet model) =>
			new QuestionSetEntity { Id = model.Id, Name = model.Name, IconUrl = model.IconUrl, Color = model.Color.Value };

		public static QuestionSet ToModel(this QuestionSetEntity entity) =>
			new QuestionSet(entity.Id, entity.Name, entity.IconUrl, new Color(entity.Color));
	}
}
