using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuizzesMapper
	{
		public static QuizDto AsQuizDto(this Quiz model) => new QuizDto
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
							IsCorrect = answer.IsCorrect
						}
					).ToArray(),
					QuestionSetId = question.QuestionSetId,
				}
			).ToArray(),
		};

		public static QuizSummaryDto AsQuizSummaryDto(this Quiz model) => new QuizSummaryDto
		{
			QuizId = model.Id,
			CorrectAnswers = model.CorrectAnswers,
			TotalQuestions = model.TotalQuestions,
			QuestionSummaries = model.Questions.Select(question =>
				new QuestionSummaryDto
				{
					QuestionId = question.Id,
					Text = question.Text,
					Answers = question.Answers.Select(answer =>
						new AnswerDto
						{
							Id = answer.Id,
							Text = answer.Text,
							IsCorrect = answer.IsCorrect
						}
					).ToArray(),
					PlayerAnswerId = question.PlayerAnswerId,
					IsAnswered = question.IsAnswered,
					IsCorrect = question.IsCorrect
				}
			).ToArray(),
		};
	}
}
