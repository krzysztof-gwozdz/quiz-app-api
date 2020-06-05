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
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				Color = model.Color.Value,
				ImageUrl = $"question-sets/{model.ImageId}/image", // TODO move to different place
				TotalQuestions = totalQuestions,
			};

		public static QuestionSetsDto AsDto(this IEnumerable<QuestionSet> model) =>
			new QuestionSetsDto
			{
				Collection = model.Select(questionSet => new QuestionSetsElementDto
				{
					Id = questionSet.Id,
					Name = questionSet.Name,
					Description = questionSet.Description,
					Color = questionSet.Color.Value,
					ImageUrl = $"question-sets/{questionSet.ImageId}/image", // TODO move to different place
				}).ToArray()
			};
	}
}
