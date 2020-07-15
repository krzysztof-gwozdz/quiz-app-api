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
					)).ToHashSet(),
				question.Tags.ToArray(),
				question.CorrectAnswersCount,
				question.AllAnswersCount
			);

		public static Question ToModel(this QuestionDocument questionDocument) =>
			new Question
			(
				questionDocument.Id,
				questionDocument.Text,
				questionDocument.Answers.Select(answerDocument =>
					new Question.Answer
					(
						answerDocument.Id,
						answerDocument.Text,
						answerDocument.IsCorrect
					)).ToHashSet(),
				questionDocument.Tags.ToHashSet(),
				questionDocument.CorrectAnswersCount,
				questionDocument.AllAnswersCount
			);

		public static ISet<Question> ToModel(this ISet<QuestionDocument> questionDocuments) =>
			new HashSet<Question>(questionDocuments.Select(ToModel));
	}
}
