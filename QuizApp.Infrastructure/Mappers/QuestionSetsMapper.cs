using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;

namespace QuizApp.Infrastructure.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSet FromEntity(this QuestionSetEntity entity) =>
			new QuestionSet(entity.Id, entity.Name, entity.IconUrl, entity.Color);
	}
}
