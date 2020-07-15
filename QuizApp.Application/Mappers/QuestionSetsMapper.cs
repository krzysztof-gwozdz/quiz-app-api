using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSetDto AsDto(this QuestionSet questionSet, int totalQuestions) =>
			new QuestionSetDto
			(
				questionSet.Id,
				questionSet.Name,
				questionSet.Description,
				questionSet.Tags.ToArray(),
				questionSet.Color.Value,
				totalQuestions
			);

		public static QuestionSetsDto AsDto(this IEnumerable<QuestionSet> questionSets) =>
			new QuestionSetsDto
			(
				questionSets.Select(questionSet => new QuestionSetsElementDto
				(
					questionSet.Id,
					questionSet.Name,
					questionSet.Tags.ToArray(),
					questionSet.Description,
					questionSet.Color.Value
				)).ToArray()
			);

		public static QuestionSetImageDto AsDto(this QuestionSetImage questionSetImage) =>
			new QuestionSetImageDto
			(
				questionSetImage.Data,
				questionSetImage.ContentType
			);
	}
}
