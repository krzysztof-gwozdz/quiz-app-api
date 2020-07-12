using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Collections.Generic;
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
				model.Tags.ToArray(),
				model.CorrectAnswersCount,
				model.AllAnswersCount,
				model.RatioOfCorrectAnswers
			);

		public static QuestionsDto AsDto(this IEnumerable<Question> model) =>
			new QuestionsDto
			(
				model.Select(question => new QuestionsElementDto
				(
					question.Id,
					question.Text,
					question.Answers.Count(),
					question.Tags.ToArray(),
					question.CorrectAnswersCount,
					question.AllAnswersCount,
					question.RatioOfCorrectAnswers
				)).ToArray()
			);
	}
}
