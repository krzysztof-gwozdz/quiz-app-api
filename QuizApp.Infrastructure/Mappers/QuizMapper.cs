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
				QuestionSetId = entity.QuestionSetId,
				Questions = entity.Questions.Select(question =>
					new QuizEntity.QuestionEntity
					{
						Id = question.Id,
						Text = question.Text,
						Answers = question.Answers.Select(answer =>
							new QuizEntity.QuestionEntity.AnswerEntity
							{
								Id = answer.Id,
								Text = answer.Text,
								IsCorrect = answer.IsCorrect,
							}).ToHashSet(),
						Tags = question.Tags.ToArray(),
						CorrectAnswerId = question.CorrectAnswerId,
						PlayerAnswerId = question.PlayerAnswerId,
						PlayerRating = question.PlayerRating
					}
				).ToHashSet()
			};

		public static Quiz ToModel(this QuizEntity entity) =>
			new Quiz(
				entity.Id,
				entity.QuestionSetId,
				entity.Questions.Select(question =>
					new Quiz.Question(
						question.Id,
						question.Text,
						question.Answers.Select(answer =>
							new Quiz.Question.Answer(
								answer.Id,
								answer.Text,
								answer.IsCorrect)
							).ToHashSet(),
						question.Tags.ToHashSet(),
						question.PlayerAnswerId,
						question.PlayerRating)
				).ToHashSet()
			);
	}
}
