using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.Mappers
{
	public static class QuestionsMapper
	{
		public static QuestionEntity ToEntity(this Question model) =>
			new QuestionEntity
			{
				Id = model.Id,
				Text = model.Text,
				Answers = model.Answers.Select(answer =>
					new QuestionEntity.AnswerEntity
					{
						Id = answer.Id,
						Text = answer.Text,
						IsCorrect = answer.IsCorrect,
					}
				).ToHashSet(),
				QuestionSetId = model.QuestionSetId
			};

		public static Question ToModel(this QuestionEntity entity) =>
			new Question(entity.Id, entity.Text, entity.Answers.Select(answer => new Question.Answer(answer.Id, answer.Text, answer.IsCorrect)).ToHashSet(), entity.QuestionSetId);

		public static ISet<Question> ToModel(this ISet<QuestionEntity> entities) =>
			new HashSet<Question>(entities.Select(ToModel));
	}
}
