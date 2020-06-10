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
				model.Color.Value,
				$"question-sets/{model.ImageId}/image", // TODO move to different place
				totalQuestions
			);

		public static QuestionSetsDto AsDto(this IEnumerable<QuestionSet> model) =>
			new QuestionSetsDto
			(
				model.Select(questionSet => new QuestionSetsElementDto
				(
					questionSet.Id,
					questionSet.Name,
					questionSet.Description,
					questionSet.Color.Value,
					$"question-sets/{questionSet.ImageId}/image" // TODO move to different place
				)).ToArray()
			);

		public static QuestionSetImageDto AsDto(this QuestionSetImage model) =>
			new QuestionSetImageDto(model.Data, model.ContentType);
	}
}
