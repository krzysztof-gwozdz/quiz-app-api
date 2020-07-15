using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;
using System.Linq;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	internal static class QuizMapper
	{
		public static QuizDocument ToDocument(this Quiz quiz) =>
			new QuizDocument
			(
				quiz.Id,
				quiz.QuestionSetId,
				quiz.Questions.Select(question =>
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

		public static Quiz ToModel(this QuizDocument quizDocument) =>
			new Quiz
			(
				quizDocument.Id,
				quizDocument.QuestionSetId,
				quizDocument.Questions.Select(questionDocument =>
					new Quiz.Question
					(
						questionDocument.Id,
						questionDocument.Text,
						questionDocument.Answers.Select(answerDocument =>
							new Quiz.Question.Answer
							(
								answerDocument.Id,
								answerDocument.Text,
								answerDocument.IsCorrect
							)).ToHashSet(),
						questionDocument.Tags.ToHashSet(),
						questionDocument.PlayerAnswerId,
						questionDocument.PlayerRating)
				).ToHashSet()
			);
	}
}
