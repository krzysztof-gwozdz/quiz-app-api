using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Tests.Examples
{
	public static class QuizExample
	{
		public static Guid NewId =>
			Guid.NewGuid();

		public static Quiz ValidQuiz =>
			new Quiz
			(
				NewId,
				new[]
				{
					Question.ValidQuestion,
					Question.ValidQuestion,
					Question.ValidQuestion,
					Question.ValidQuestion
				}
			);

		public static class Question
		{
			public static Guid NewId =>
				Guid.NewGuid();

			public static string ValidText =>
				Guid.NewGuid().ToString();

			public static HashSet<Quiz.Question.Answer> ValidAnswers =>
				new HashSet<Quiz.Question.Answer> { Answer.ValidAnswer, Answer.ValidAnswer, Answer.ValidAnswer, Answer.ValidAnswer };

			public static Quiz.Question ValidQuestion
			{
				get
				{
					var answers = ValidAnswers;
					return new Quiz.Question(NewId, ValidText, answers, answers.First().Id, null, QuestionSetExample.NewId);
				}
			}

			public static class Answer
			{
				public static string ValidText =>
					Guid.NewGuid().ToString();

				public static Quiz.Question.Answer ValidAnswer =>
					new Quiz.Question.Answer(Guid.NewGuid(), Guid.NewGuid().ToString());
			}
		}
	}
}
