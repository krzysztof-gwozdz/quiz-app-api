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

		public static ISet<string> ValidTags =>
			new HashSet<string>(new[] { Guid.NewGuid().ToString() });

		public static Question GetValidQuestion(int answerCount) =>
			new Question(NewId, ValidText, Answer.GetValidAnswers(answerCount), ValidTags);

		public static HashSet<Question> GetValidQuestions(int questionCount, int answerCount) =>
			Enumerable.Range(0, questionCount).Select(x => GetValidQuestion(answerCount)).ToHashSet();

		public static class Answer
		{
			public static Guid NewId =>
				Guid.NewGuid();

			public static string ValidText =>
				Guid.NewGuid().ToString();

			public static Question.Answer ValidInCorrectAnswer =>
				new Question.Answer(NewId, ValidText, false);

			public static Question.Answer ValidCorrectAnswer =>
				new Question.Answer(NewId, ValidText, true);

			public static HashSet<Question.Answer> GetValidAnswers(int count)
			{
				var answers = Enumerable.Range(1, count).Select(x => ValidInCorrectAnswer).ToHashSet();
				answers.Add(ValidCorrectAnswer);
				return answers;
			}
		}
	}
}
