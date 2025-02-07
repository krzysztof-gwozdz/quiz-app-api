﻿using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.Tests.Examples
{
	public static class QuizExample
	{
		public static Guid ValidId =>
			Guid.NewGuid();

		public static Guid ValidQuestionSetId =>
			Guid.NewGuid();

		public static Quiz GetValidQuiz(int questionCount, int answerCount) =>
			new Quiz
			(
				ValidId,
				ValidQuestionSetId,
				Question.GetValidQuestions(questionCount, answerCount)
			);

		public static class Question
		{
			public static Guid ValidId =>
				Guid.NewGuid();

			public static string ValidText =>
				Guid.NewGuid().ToString();
			public static ISet<string> ValidTags =>
				new HashSet<string>(new[] { Guid.NewGuid().ToString() });

			public static Quiz.Question GetValidQuestion(int answerCount) =>
				new Quiz.Question(ValidId, ValidText, Answer.GetValidAnswers(answerCount), ValidTags, null, null);

			public static HashSet<Quiz.Question> GetValidQuestions(int questionCount, int answerCount) =>
				Enumerable.Range(0, questionCount).Select(x => GetValidQuestion(answerCount)).ToHashSet();

			public static class Answer
			{
				public static Guid ValidId =>
					Guid.NewGuid();

				public static string ValidText =>
					Guid.NewGuid().ToString();

				public static bool ValidIsCorrect =>
					false;

				public static Quiz.Question.Answer ValidAnswer =>
					new Quiz.Question.Answer(ValidId, ValidText, ValidIsCorrect);

				public static HashSet<Quiz.Question.Answer> GetValidAnswers(int answerCount)
				{
					var answers = Enumerable.Range(0, answerCount - 1).Select(x => ValidAnswer).ToHashSet();
					answers.Add(new Quiz.Question.Answer(ValidId, ValidText, true));
					return answers;
				}
			}
		}

		public static class PlayerAnswer
		{
			public static Guid NewQuestionId =>
				Guid.NewGuid();

			public static Guid NewAnswerId =>
				Guid.NewGuid();

			public static QuestionRatings? ValidRating => QuestionRatings.Positive;
		}
	}
}
