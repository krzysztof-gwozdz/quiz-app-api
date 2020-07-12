using QuizApp.Core.Models;
using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class InvalidPasswordException : DomainException
	{
		public static readonly string Requirements =
			$"Password must:{Environment.NewLine}" +
			$"Be a minimum of {Password.MinimumLength} characters in length{Environment.NewLine}" +
			$"Be a maximum of {Password.MaximumLength} characters in length{Environment.NewLine}" +
			$"Contains at least one uppercase letter (A-Z){Environment.NewLine}" +
			$"Contains at least one lowercase letter (a-z){Environment.NewLine}" +
			$"Contains at least one digit (0-9){Environment.NewLine}" +
			$"Contains at least one special character ({Password.AllowedSpecialCharacters})";

		public override string Code => "invalid_password";

		public InvalidPasswordException() : base($"Invalid password. {Requirements}")
		{
		}
	}
}
