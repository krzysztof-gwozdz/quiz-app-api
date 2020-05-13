using QuizApp.Core.Models;
using QuizApp.Infrastructure.Entities;
using System.Linq;

namespace QuizApp.Infrastructure.Mappers
{
	public static class QuizMapper
	{
		public static QuizEntity ToEntity(this Quiz entity) =>
			new QuizEntity
			{
				Id = entity.Id,
				Questions = entity.Questions.Select(question =>
					new QuizEntity.QuestionEntity
					{
						Id = question.Id,
						Text = question.Text,
						Answers = question.Answers.Select(answer =>
							new QuizEntity.QuestionEntity.AnswerEntity
							{
								Id = answer.Id,
								Text = answer.Text
							}).ToHashSet(),
						CorrectAnswerId = question.CorrectAnswerId,
						QuestionSetId = question.QuestionSetId,
						PlayerAnswerId = question.PlayerAnswerId
					}
				).ToHashSet()
			};

		public static Quiz FromEntity(this QuizEntity entity) =>
			new Quiz(
				entity.Id,
				entity.Questions.Select(question =>
					new Quiz.Question(
						question.Id,
						question.Text,
						question.Answers.Select(answer =>
							new Quiz.Question.Answer(
								answer.Id,
								answer.Text)
							).ToHashSet(),
						question.CorrectAnswerId,
						question.PlayerAnswerId,
						question.QuestionSetId)
				).ToArray()
			);
	}
}
