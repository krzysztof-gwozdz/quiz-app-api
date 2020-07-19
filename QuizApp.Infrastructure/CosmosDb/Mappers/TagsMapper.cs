using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	internal static class TagsMapper
	{
		public static TagDocument ToDocument(this Tag tag) =>
			new TagDocument
			(
				tag.Id,
				tag.Name,
				tag.Description
				);

		public static Tag ToModel(this TagDocument tagDocument) =>
			new Tag
			(
				tagDocument.Id,
				tagDocument.Name,
				tagDocument.Description
			);

		public static ISet<Tag> ToModel(this ISet<TagDocument> tagDocuments) =>
			new HashSet<Tag>(tagDocuments.Select(ToModel));
	}
}
