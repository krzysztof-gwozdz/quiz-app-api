using QuizApp.Core.Models;
using System;
using System.Collections.Generic;

namespace QuizApp.Core.Tests.Examples
{
	public static class QuestionSetExample
	{
		public static Guid ValidId =>
			Guid.NewGuid();

		public static string ValidName =>
			Guid.NewGuid().ToString();

		public static ISet<string> ValidTags =>
			new HashSet<string>(new[] { Guid.NewGuid().ToString() });

		public static string ValidDescription =>
			Guid.NewGuid().ToString();

		public static Guid ValidImageId =>
			Guid.NewGuid();

		public static Color ValidColor =>
			Color.Create("#FFFFFF");

		public static QuestionSet ValidQuestionSet =>
			new QuestionSet(ValidId, ValidName, ValidTags, ValidDescription, ValidImageId, ValidColor);
	}
}
