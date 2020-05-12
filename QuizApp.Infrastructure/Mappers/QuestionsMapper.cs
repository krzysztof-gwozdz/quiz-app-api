using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;
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
					new AnswerEntity
					{
						Id = answer.Id,
						Text = answer.Text,
					}
				).ToHashSet(), 
				CorrectAnswerId = model.CorrectAnswerId 
			};

		public static Question FromEntity(this QuestionEntity entity) =>
			new Question(entity.Id, entity.Text, entity.Answers.Select(answer => new Answer(answer.Id, answer.Text)).ToHashSet(), entity.CorrectAnswerId);
	}
}
