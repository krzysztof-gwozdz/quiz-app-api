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
				Color = model.Color.Value,
				IconUrl = $"question-sets/{model.IconId}/icon", // TODO move to different place
				TotalQuestions = totalQuestions,
			};

		public static QuestionSetsDto AsDto(this IEnumerable<QuestionSet> model) =>
			new QuestionSetsDto
			{
				Collection = model.Select(questionSet => new QuestionSetsElementDto
				{
					Id = questionSet.Id,
					Name = questionSet.Name,
					Color = questionSet.Color.Value,
					IconUrl = $"question-sets/{questionSet.IconId}/icon", // TODO move to different place
				}).ToArray()
			};
	}
}
