using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuestionsMapper
	{
		public static QuestionDto AsDto(this Question model) =>
			new QuestionDto
			{
				Id = model.Id,
				Text = model.Text,
				Answers = model.Answers.Select(answer =>
					new AnswerDto
					{
						Id = answer.Id,
						Text = answer.Text,
						IsCorrect = answer.IsCorrect
					}
				).ToArray(),
				QuestionSetId = model.QuestionSetId,
			};
	}
}
