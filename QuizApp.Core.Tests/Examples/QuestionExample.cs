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

		public static Question GetValidQuestion(int answerCount)
		{
			var answers = Answer.GetValidAnswers(answerCount);
			return new Question(NewId, ValidText, answers, answers.First().Id, QuestionSetExample.NewId);
		}

		public static HashSet<Question> GetValidQuestions(int questionCount, int answerCount) =>
			Enumerable.Range(0, questionCount).Select(x => GetValidQuestion(answerCount)).ToHashSet();

		public static class Answer
		{
			public static Guid NewId =>
				Guid.NewGuid();

			public static string ValidText =>
				Guid.NewGuid().ToString();

			public static Question.Answer ValidAnswer =>
				new Question.Answer(NewId, ValidText);

			public static HashSet<Question.Answer> GetValidAnswers(int count) =>
				Enumerable.Range(0, count).Select(x => ValidAnswer).ToHashSet();
		}
	}
}
