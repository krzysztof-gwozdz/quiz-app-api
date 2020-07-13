using FluentAssertions;
using QuizApp.Application.Services;
using QuizApp.Core.Tests.Examples;
using Xunit;

namespace QuizApp.Application.Tests.Services
{
	public class PasswordsServiceTests
	{
		private readonly PasswordsService _passwordsService;

		public PasswordsServiceTests()
		{
			_passwordsService = new PasswordsService();
		}

		[Fact]
		public void GenerateSalt_SaltWithCorrectLength()
		{
			//arrange

			//act 
			var salt = _passwordsService.GenerateSalt();

			//assert
			salt.Should().NotBeEmpty();
			salt.Should().HaveCount(128 / 8);
		}

		[Fact]
		public void HashPassword_HashedPassword()
		{
			//arrange
			var password = PasswordExample.ValidPassword;
			var salt = IdentityExample.ValidSalt;

			//act 
			var passwordHash = _passwordsService.HashPassword(password.Value, salt);

			//assert
			passwordHash.Should().Be(IdentityExample.ValidPasswordHash);
		}
	}
}
