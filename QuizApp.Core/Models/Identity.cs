using QuizApp.Core.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuizApp.Core.Models
{
	public class Identity
	{
		public const byte MinimumUsernameLength = 5;
		public const byte MaximumUsernameLength = 32;
		public const string AllowedSpecialCharactersInUsername = @"~!@#$%^&*_\-.";
		public static readonly string Pattern = $"^[A-Za-z\\d{AllowedSpecialCharactersInUsername}]" + "{" + MinimumUsernameLength + "," + MaximumUsernameLength + "}$";

		public Guid Id { get; }
		public string Username { get; }
		public string PasswordHash { get; }
		public byte[] Salt { get; }

		public Identity(Guid id, string username, string passwordHash, byte[] salt)
		{
			Id = id;
			Username = username;
			PasswordHash = passwordHash;
			Salt = salt;
		}

		public static Identity Create(string username, string passwordHash, byte[] salt)
		{
			if (string.IsNullOrWhiteSpace(username))
				throw new EmptyIdentityUsernameException();

			var regex = new Regex(Pattern);
			if (!regex.IsMatch(username))
				throw new InvalidIdentityUsernameException(username);

			if (string.IsNullOrWhiteSpace(passwordHash))
				throw new EmptyIdentityPasswordHashException();

			if (salt is null || !salt.Any())
				throw new EmptyIdentitySaltException();

			return new Identity(Guid.NewGuid(), username, passwordHash, salt);
		}
	}
}
