using FluentAssertions;
using QuizApp.Application.Services;
using Xunit;

namespace QuizApp.Application.Tests.Services
{
	public class PasswordsServiceTests
	{
		[Fact]
		public void GenerateSalt_SaltWithCorrectLength()
		{
			//arrange
			var passwordsService = new PasswordsService();

			//act 
			var salt = passwordsService.GenerateSalt();

			//assert
			salt.Should().NotBeEmpty();
			salt.Should().HaveCount(128 / 8);
		}

		[Fact]
		public void HashPassword_HashedPassword()
		{
			//arrange
			var passwordsService = new PasswordsService();
			var password = "password";
			var salt = new byte[128 / 8] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

			//act 
			var passwordHash = passwordsService.HashPassword(password, salt);

			//assert
			passwordHash.Should().Be("jj4vc8PrY5CoGrvIEBwDQ7AXp6//+1q2XhNPCQncyiw=");
		}
	}
}
