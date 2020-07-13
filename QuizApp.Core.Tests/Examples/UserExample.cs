using QuizApp.Core.Models;
using System;

namespace QuizApp.Core.Tests.Examples
{
	public class UserExample
	{
		public static Guid ValidId
			=> Guid.NewGuid();

		public static string ValidUsername
			=> "admin";

		public static string ValidPasswordHash
			=> "ydua6fcCGrw63yBbs8N65TDk+6mZYxzUe3rGk7UiLQA=";

		public static byte[] ValidSalt
			=> new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

		public static User ValidUser
			=> new User(ValidId, ValidUsername, ValidPasswordHash, ValidSalt);
	}
}
