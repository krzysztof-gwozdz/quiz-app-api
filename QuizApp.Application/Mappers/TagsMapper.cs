using QuizApp.Application.Dtos;
using QuizApp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Application.Mappers
{
	public static class TagsMapper
	{
		public static TagDto AsDto(this Tag tag) =>
			new TagDto
			(
				tag.Name,
				tag.Description
			);

		public static TagsDto AsDto(this IEnumerable<Tag> tags) =>
			new TagsDto
			(
				tags.Select(tag => new TagDtosElementDto
				(
					tag.Name,
					tag.Description
				)).ToArray()
			);
	}
}
