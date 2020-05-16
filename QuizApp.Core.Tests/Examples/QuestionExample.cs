using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Tests.Examples
{
	public static class QuestionExample
	{
		public static Guid NewId =>
			Guid.NewGuid();

		public static string ValidText =>
			Guid.NewGuid().ToString();

		public static HashSet<Question.Answer> ValidAnswers =>
			new HashSet<Question.Answer> { Answer.ValidAnswer, Answer.ValidAnswer, Answer.ValidAnswer, Answer.ValidAnswer };

		public static Question ValidQuestion
		{
			get
			{
				var answers = ValidAnswers;
				return new Question(NewId, ValidText, answers, answers.First().Id, QuestionSetExample.NewId);
			}
		}

		public static class Answer
		{
			public static string ValidText =>
				Guid.NewGuid().ToString();

			public static Question.Answer ValidAnswer =>
				new Question.Answer(Guid.NewGuid(), Guid.NewGuid().ToString());
		}
	}
}
