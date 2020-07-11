using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	public static class QuestionsMapper
	{
		public static QuestionDocument ToDocument(this Question question) =>
			new QuestionDocument
			(
				question.Id,
				question.Text,
				question.Answers.Select(answer =>
					new QuestionDocument.AnswerDocument
					(
						answer.Id,
						answer.Text,
						answer.IsCorrect
					)
				).ToHashSet(),
				question.Tags.ToArray(),
				question.CorrectAnswersCount,
				question.AllAnswersCount
			);

		public static Question ToModel(this QuestionDocument document) =>
			new Question(
				document.Id,
				document.Text,
				document.Answers.Select(answer => new Question.Answer(answer.Id, answer.Text, answer.IsCorrect)).ToHashSet(),
				document.Tags.ToHashSet(),
				document.CorrectAnswersCount,
				document.AllAnswersCount
			);

		public static ISet<Question> ToModel(this ISet<QuestionDocument> documents) =>
			new HashSet<Question>(documents.Select(ToModel));
	}
}
