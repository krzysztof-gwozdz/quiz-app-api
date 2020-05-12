using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSetDto AsDto(this QuestionSet model) =>
			new QuestionSetDto
			{
				Id = model.Id,
				Name = model.Name,
				IconUrl = model.IconUrl,
				Color = model.Color,
				// TODO TotalQuestions
			};

		public static QuestionSetsDto AsDto(this IEnumerable<QuestionSet> model) =>
			new QuestionSetsDto
			{
				Collection = model.Select(questionSet => new QuestionSetsElementDto
				{
					Id = questionSet.Id,
					Name = questionSet.Name,
					IconUrl = questionSet.IconUrl,
					Color = questionSet.Color,
				}).ToArray()
			};
	}
}
