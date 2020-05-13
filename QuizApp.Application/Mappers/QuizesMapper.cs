using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuizesMapper
	{
		public static QuizDto AsDto(this Quiz model) => new QuizDto
		{
			Id = model.Id,
			Questions = model.Questions.Select(question =>
				new QuestionDto
				{
					Id = question.Id,
					Text = question.Text,
					Answers = question.Answers.Select(answer =>
						new AnswerDto
						{
							Id = answer.Id,
							Text = answer.Text,
						}
					).ToArray(),
					CorrectAnswerId = question.CorrectAnswerId,
					QuestionSetId = question.QuestionSetId,
				}
				).ToArray(),
		};
	}
}
