using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class TagsMapper
	{
		public static TagsDto AsDto(this IEnumerable<Tag> model) =>
			new TagsDto
			(
				model.Select(tag => new TagDto
				(
					tag.Id,
					tag.Name
				)).ToArray()
			);
	}
}
