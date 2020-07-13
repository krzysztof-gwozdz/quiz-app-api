using QuizApp.Core.Models;
using QuizApp.Shared;
using System;
using System.Globalization;
using System.IO;

namespace QuizApp.Core.Tests.Examples
{
	public static class QuestionSetImageExample
	{
		public static Guid ValidId =>
			Guid.NewGuid();

		public static Stream ValidData =>
			new MemoryStream(new[] { byte.Parse("FF", NumberStyles.AllowHexSpecifier), byte.Parse("D8", NumberStyles.HexNumber), });

		public static string ValidContentType =>
			ContentTypes.Image.Jpeg;

		public static QuestionSetImage ValidQuestionSetImage =>
			new QuestionSetImage(ValidId, ValidData, ValidContentType);
	}
}
