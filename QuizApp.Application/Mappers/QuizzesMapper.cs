﻿using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuizzesMapper
	{
		public static QuizDto AsQuizDto(this Quiz model) => new QuizDto
		(
			model.Id,
			model.Questions.Select(question =>
				new QuestionDto
				(
					question.Id,
					question.Text,
					question.Answers.Select(answer => new AnswerDto(answer.Id, answer.Text, answer.IsCorrect)).ToArray(),
					question.QuestionSetId
				)
			).ToArray()
		);

		public static QuizSummaryDto AsQuizSummaryDto(this Quiz model) => new QuizSummaryDto
		(
			 model.Id,
			 model.CorrectAnswers,
			 model.TotalQuestions,
			 model.Questions.Select(question =>
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
