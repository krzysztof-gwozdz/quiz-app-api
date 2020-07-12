using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSetDto AsDto(this QuestionSet model, int totalQuestions) =>
			new QuestionSetDto
			(
				model.Id,
				model.Name,
				model.Description,
				model.Tags.ToArray(),
				model.Color.Value,
				totalQuestions
			);

		public static QuestionSetsDto AsDto(this IEnumerable<QuestionSet> model) =>
			new QuestionSetsDto
			(
				model.Select(questionSet => new QuestionSetsElementDto
				(
					questionSet.Id,
					questionSet.Name,
					questionSet.Tags.ToArray(),
					questionSet.Description,
					questionSet.Color.Value
				)).ToArray()
			);

		public static QuestionSetImageDto AsDto(this QuestionSetImage model) =>
			new QuestionSetImageDto(model.Data, model.ContentType);
	}
}
