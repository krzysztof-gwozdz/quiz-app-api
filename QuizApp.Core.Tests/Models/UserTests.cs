using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using System;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class UserTests
	{
		[Fact]
		public void CreateUserWithCorrectValues_UserCreated()
		{
			//arrange
			var username = UserExample.ValidUsername;
			var passwordHash = UserExample.ValidPasswordHash;
			var salt = UserExample.ValidSalt;

			//act
			var user = User.Create(username, passwordHash, salt);

			//assert
			user.Username.Should().Be(username);
			user.PasswordHash.Should().Be(passwordHash);
			user.Salt.Should().BeEquivalentTo(salt);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateUserWithEmptyUsername_ThrowException(string username)
		{
			//arrange
			var passwordHash = UserExample.ValidPasswordHash;
			var salt = UserExample.ValidSalt;

			//act
			Action createUser = () => User.Create(username, passwordHash, salt);

			//assert
			createUser.Should().Throw<EmptyUserUsernameException>()
				.WithMessage("User username can not be empty.");
		}

		[Theory]
		[InlineData("Aa12")]
		[InlineData("123456789012345678901234567890123")]
		public void CreateUserWithInvalidUsername_ThrowException(string username)
		{
			//arrange
			var passwordHash = UserExample.ValidPasswordHash;
			var salt = UserExample.ValidSalt;

			//act
			Action createUser = () => User.Create(username, passwordHash, salt);

			//assert
			createUser.Should().Throw<InvalidUserUsernameException>()
				.WithMessage($"User username: {username} is invalid. {InvalidUserUsernameException.Requirements}");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void CreateUserWithEmptyHash_ThrowException(string passwordHash)
		{
			//arrange
			var username = UserExample.ValidUsername;
			var salt = UserExample.ValidSalt;

			//act
			Action createUser = () => User.Create(username, passwordHash, salt);

			//assert
			createUser.Should().Throw<EmptyUserPasswordHashException>()
				.WithMessage("User password hash can not be empty.");
		}

		[Fact]
		public void CreateUserWithSaltThatDoesNotExist_ThrowException()
		{
			//arrange
			var username = UserExample.ValidUsername;
			var passwordHash = UserExample.ValidPasswordHash;
			var salt = (byte[])null;

			//act
			Action createUser = () => User.Create(username, passwordHash, salt);

			//assert
			createUser.Should().Throw<EmptyUserSaltException>()
				.WithMessage("User salt can not be empty.");
		}

		[Fact]
		public void CreateUserWithEmptySalt_ThrowException()
		{
			//arrange
			var username = UserExample.ValidUsername;
			var passwordHash = UserExample.ValidPasswordHash;
			var salt = Array.Empty<byte>();

			//act
			Action createUser = () => User.Create(username, passwordHash, salt);

			//assert
			createUser.Should().Throw<EmptyUserSaltException>()
				.WithMessage("User salt can not be empty.");
		}
	}
}
