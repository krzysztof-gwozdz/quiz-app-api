using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuestionsMapper
	{
		public static QuestionDto AsDto(this Question question) =>
			new QuestionDto
			(
				question.Id,
				question.Text,
				question.Answers.Select(answer => new AnswerDto(answer.Id, answer.Text, answer.IsCorrect)).ToArray(),
				question.Tags.ToArray(),
				question.CorrectAnswersCount,
				question.AllAnswersCount,
				question.RatioOfCorrectAnswers
			);

		public static QuestionsDto AsDto(this IEnumerable<Question> questions) =>
			new QuestionsDto
			(
				questions.Select(question => new QuestionsElementDto
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
