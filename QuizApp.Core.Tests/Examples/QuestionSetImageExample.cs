using QuizApp.Core.Models;
using System;
using System.IO;
using QuizApp.Shared;

namespace QuizApp.Core.Tests.Examples
{
	public static class QuestionSetImageExample
	{
		public static Guid NewId =>
			Guid.NewGuid();

		public static Stream ValidData =>
			new MemoryStream(new byte[100]);

		public static string ValidContentType =>
			MediaTypes.Image.Jpeg;

		public static QuestionSetImage ValidQuestionSetImage =>
			new QuestionSetImage(NewId, ValidData);
	}
}
