using QuizApp.Core.Models;
using System;
using System.Globalization;
using System.IO;
using QuizApp.Shared;

namespace QuizApp.Core.Tests.Examples
{
	public static class QuestionSetImageExample
	{
		public static Guid NewId =>
			Guid.NewGuid();

		public static Stream ValidData =>
			new MemoryStream(new[] { byte.Parse("FF", NumberStyles.AllowHexSpecifier), byte.Parse("D8", NumberStyles.HexNumber), });

		public static string ValidContentType =>
			ContentTypes.Image.Jpeg;

		public static QuestionSetImage ValidQuestionSetImage =>
			new QuestionSetImage(NewId, ValidData, ValidContentType);
	}
}
