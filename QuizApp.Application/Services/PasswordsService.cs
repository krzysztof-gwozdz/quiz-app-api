using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using QuizApp.Core.Models;
using System;
using System.Security.Cryptography;

namespace QuizApp.Application.Services
{
	public class PasswordsService : IPasswordsService
	{
		public byte[] GenerateSalt()
		{
			var salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
				rng.GetBytes(salt);

			return salt;
		}

		public string HashPassword(Password password, byte[] salt)
		{
			return Convert.ToBase64String(KeyDerivation.Pbkdf2(password.Value, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
		}
	}
}
