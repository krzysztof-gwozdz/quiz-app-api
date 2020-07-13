using QuizApp.Core.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuizApp.Core.Models
{
	public class User
	{
		public const byte MinimumUsernameLength = 5;
		public const byte MaximumUsernameLength = 32;
		public const string AllowedSpecialCharactersInUsername = @"~!@#$%^&*_\-.";
		public static readonly string Pattern = $"^[A-Za-z\\d{AllowedSpecialCharactersInUsername}]" + "{" + MinimumUsernameLength + "," + MaximumUsernameLength + "}$";

		public Guid Id { get; }
		public string Username { get; }
		public string PasswordHash { get; }
		public byte[] Salt { get; }

		public User(Guid id, string username, string passwordHash, byte[] salt)
		{
			Id = id;
			Username = username;
			PasswordHash = passwordHash;
			Salt = salt;
		}

		public static User Create(string username, string passwordHash, byte[] salt)
		{
			if (string.IsNullOrWhiteSpace(username))
				throw new EmptyUserUsernameException();

			var regex = new Regex(Pattern);
			if (!regex.IsMatch(username))
				throw new InvalidUserUsernameException(username);

			if (string.IsNullOrWhiteSpace(passwordHash))
				throw new EmptyUserPasswordHashException();

			if (salt is null || !salt.Any())
				throw new EmptyUserSaltException();

			return new User(Guid.NewGuid(), username, passwordHash, salt);
		}
	}
}
