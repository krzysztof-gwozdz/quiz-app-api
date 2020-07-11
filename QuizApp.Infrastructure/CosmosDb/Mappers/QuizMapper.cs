using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;
using System.Linq;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	public static class QuizMapper
	{
		public static QuizDocument ToDocument(this Quiz document) =>
			new QuizDocument
			(
				document.Id,
				 document.QuestionSetId,
				 document.Questions.Select(question =>
					new QuizDocument.QuestionDocument
					(
						 question.Id,
						 question.Text,
						 question.Answers.Select(answer =>
							new QuizDocument.QuestionDocument.AnswerDocument
							(
								 answer.Id,
								 answer.Text,
								 answer.IsCorrect
							)).ToHashSet(),
						 question.Tags.ToArray(),
						 question.CorrectAnswerId,
						 question.PlayerAnswerId,
						 question.PlayerRating
					)
				).ToHashSet()
			);

		public static Quiz ToModel(this QuizDocument document) =>
			new Quiz(
				document.Id,
				document.QuestionSetId,
				document.Questions.Select(question =>
					new Quiz.Question(
						question.Id,
						question.Text,
						question.Answers.Select(answer =>
							new Quiz.Question.Answer(
								answer.Id,
								answer.Text,
								answer.IsCorrect)
							).ToHashSet(),
						question.Tags.ToHashSet(),
						question.PlayerAnswerId,
						question.PlayerRating)
				).ToHashSet()
			);
	}
}
