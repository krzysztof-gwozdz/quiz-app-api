using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	public static class QuestionSetsMapper
	{
		public static QuestionSetDocuments ToDocument(this QuestionSet model) =>
			new QuestionSetDocuments
			(
				model.Id,
				model.Name,
				model.Description,
				model.Tags.ToArray(),
				model.ImageId,
				model.Color.Value
			);

		public static QuestionSet ToModel(this QuestionSetDocuments document) =>
			new QuestionSet
			(
				document.Id,
				document.Name,
				document.Description,
				document.Tags.ToHashSet(),
				document.ImageId,
				new Color(document.Color)
			);

		public static ISet<QuestionSet> ToModel(this ISet<QuestionSetDocuments> documents) =>
			new HashSet<QuestionSet>(documents.Select(ToModel));
	}
}
