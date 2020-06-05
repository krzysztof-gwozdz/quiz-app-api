using QuizApp.Core.Models;
using System;

namespace QuizApp.Core.Tests.Examples
{
	public static class QuestionSetExample
	{
		public static Guid NewId =>
			Guid.NewGuid();

		public static string ValidName =>
			Guid.NewGuid().ToString();

		public static string ValidDescription =>
			Guid.NewGuid().ToString();

		public static Guid ValidIconId =>
			Guid.NewGuid();

		public static Color ValidColor =>
			Color.Create("#FFFFFF");

		public static QuestionSet ValidQuestionSet =>
			new QuestionSet(NewId, ValidName, ValidDescription, ValidIconId, ValidColor);
	}
}
