using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	public static class TagsMapper
	{
		public static TagDocument ToDocument(this Tag model) =>
			new TagDocument { Name = model.Name, Description = model.Description };

		public static Tag ToModel(this TagDocument document) =>
			new Tag(document.Name, document.Description);

		public static ISet<Tag> ToModel(this ISet<TagDocument> documents) =>
			new HashSet<Tag>(documents.Select(ToModel));
	}
}
