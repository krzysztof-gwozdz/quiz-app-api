using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Collections.Generic;
using System.Linq;
using QuizApp.Shared;

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
				$"{AppContext.AppBaseUrl}/question-sets/{model.ImageId}/image", // TODO move to different place, it's like rest but use image id...
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
					$"{AppContext.AppBaseUrl}/question-sets/{questionSet.ImageId}/image" // TODO move to different place, it's like rest but use image id...
				)).ToArray()
			);

		public static QuestionSetImageDto AsDto(this QuestionSetImage model) =>
			new QuestionSetImageDto(model.Data, model.ContentType);
	}
}
