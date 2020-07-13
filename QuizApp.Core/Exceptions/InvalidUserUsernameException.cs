using QuizApp.Core.Models;
using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class InvalidUserUsernameException : DomainException
	{
		public static readonly string Requirements =
			$"Username must:{Environment.NewLine}" +
			$"Be a minimum of {User.MinimumUsernameLength} characters in length{Environment.NewLine}" +
			$"Be a maximum of {User.MaximumUsernameLength} characters in length{Environment.NewLine}" +
			$"Contains only uppercase letters (A-Z), lowercase letters (a-z) digits (0-9) and special characters ({Password.AllowedSpecialCharacters}).";

		public override string Code => "invalid_user_username";

		public InvalidUserUsernameException(string username) : base($"User username: {username} is invalid. {Requirements}")
		{
		}
	}
}
