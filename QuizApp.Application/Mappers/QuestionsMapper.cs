using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuestionsMapper
	{
		public static QuestionDto AsDto(this Question model) =>
			new QuestionDto
			(
				model.Id,
				model.Text,
				model.Answers.Select(answer => new AnswerDto(answer.Id, answer.Text, answer.IsCorrect)).ToArray(),
				model.QuestionSetId,
				model.Tags.ToArray()
			);
	}
}
