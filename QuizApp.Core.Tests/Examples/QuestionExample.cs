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

		public static Question ValidQuestion
		{
			get
			{
				var answers = Answer.GetValidAnswers(4);
				return new Question(NewId, ValidText, answers, answers.First().Id, QuestionSetExample.NewId);
			}
		}

		public static HashSet<Question> GetQuestions(int count) =>
			Enumerable.Range(0, count).Select(x => ValidQuestion).ToHashSet();

		public static class Answer
		{
			public static string ValidText =>
				Guid.NewGuid().ToString();

			public static Question.Answer ValidAnswer =>
				new Question.Answer(Guid.NewGuid(), Guid.NewGuid().ToString());

			public static HashSet<Question.Answer> GetValidAnswers(int count) =>
				Enumerable.Range(0, count).Select(x => ValidAnswer).ToHashSet();
		}
	}
}
