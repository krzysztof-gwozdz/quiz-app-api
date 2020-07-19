using QuizApp.Core.Models;
using System;

namespace QuizApp.Core.Tests.Examples
{
	public static class TagExample
	{
		public static Guid ValidId =>
			Guid.NewGuid();

		public static string ValidName =>
			Guid.NewGuid().ToString();

		public static string ValidDescription =>
			Guid.NewGuid().ToString();

		public static Tag ValidTag =>
			new Tag(ValidId, ValidName, ValidDescription);
	}
}
