using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuizzesMapper
	{
		public static QuizDto AsQuizDto(this Quiz quiz, string questionSetName) => new QuizDto
		(
			quiz.Id,
			quiz.QuestionSetId,
			questionSetName,
			quiz.Questions.Select(question =>
				new QuizQuestionDto
				(
					question.Id,
					question.Text,
					question.Answers.Select(answer => new QuizAnswerDto(answer.Id, answer.Text, answer.IsCorrect)).ToArray(),
					question.Tags.ToArray()
				)
			).ToArray()
		);

		public static QuizSummaryDto AsQuizSummaryDto(this Quiz quiz, string questionSetName) => new QuizSummaryDto
		(
			quiz.Id,
			quiz.QuestionSetId,
			questionSetName,
			quiz.CorrectAnswers,
			quiz.TotalQuestions,
			quiz.Questions.Select(question =>
				new QuestionSummaryDto
				(
					question.Id,
					question.Text,
					question.Answers.Select(answer => new AnswerDto(answer.Id, answer.Text, answer.IsCorrect)).ToArray(),
					question.CorrectAnswerId,
					question.PlayerAnswerId,
					question.IsAnswered,
					question.IsCorrect
				)
			).ToArray()
		);
	}
}
