using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	internal static class QuestionSetsMapper
	{
		public static QuestionSetDocuments ToDocument(this QuestionSet questionSet) =>
			new QuestionSetDocuments
			(
				questionSet.Id,
				questionSet.Name,
				questionSet.Tags.ToArray(),
				questionSet.Description,
				questionSet.ImageId,
				questionSet.Color.Value
			);

		public static QuestionSet ToModel(this QuestionSetDocuments questionSetDocument) =>
			new QuestionSet
			(
				questionSetDocument.Id,
				questionSetDocument.Name,
				questionSetDocument.Tags.ToHashSet(),
				questionSetDocument.Description,
				questionSetDocument.ImageId,
				new Color(questionSetDocument.Color)
			);

		public static ISet<QuestionSet> ToModel(this ISet<QuestionSetDocuments> questionSetDocuments) =>
			new HashSet<QuestionSet>(questionSetDocuments.Select(ToModel));
	}
}
