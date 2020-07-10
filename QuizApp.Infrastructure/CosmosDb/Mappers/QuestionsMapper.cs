using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	public static class QuestionsMapper
	{
		public static QuestionDocument ToDocument(this Question model) =>
			new QuestionDocument
			{
				Id = model.Id,
				Text = model.Text,
				Answers = model.Answers.Select(answer =>
					new QuestionDocument.AnswerDocument
					{
						Id = answer.Id,
						Text = answer.Text,
						IsCorrect = answer.IsCorrect,
					}
				).ToHashSet(),
				Tags = model.Tags.ToArray()
			};

		public static Question ToModel(this QuestionDocument document) =>
			new Question(document.Id, document.Text, document.Answers.Select(answer => new Question.Answer(answer.Id, answer.Text, answer.IsCorrect)).ToHashSet(), document.Tags.ToHashSet());

		public static ISet<Question> ToModel(this ISet<QuestionDocument> documents) =>
			new HashSet<Question>(documents.Select(ToModel));
	}
}
