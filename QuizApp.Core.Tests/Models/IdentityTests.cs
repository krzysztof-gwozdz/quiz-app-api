using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using System;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class IdentityTests
	{
		[Fact]
		public void CreateIdentityWithCorrectValues_IdentityCreated()
		{
			//arrange
			var username = IdentityExample.ValidUsername;
			var passwordHash = IdentityExample.ValidPasswordHash;
			var salt = IdentityExample.ValidSalt;

			//act
			var identity = Identity.Create(username, passwordHash, salt);

			//assert
			identity.Username.Should().Be(username);
			identity.PasswordHash.Should().Be(passwordHash);
			identity.Salt.Should().BeEquivalentTo(salt);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateIdentityWithEmptyUsername_ThrowException(string username)
		{
			//arrange
			var passwordHash = IdentityExample.ValidPasswordHash;
			var salt = IdentityExample.ValidSalt;

			//act
			Action createIdentity = () => Identity.Create(username, passwordHash, salt);

			//assert
			createIdentity.Should().Throw<EmptyIdentityUsernameException>()
				.WithMessage("Identity username can not be empty.");
		}

		[Theory]
		[InlineData("Aa12")]
		[InlineData("123456789012345678901234567890123")]
		public void CreateIdentityWithInvalidUsername_ThrowException(string username)
		{
			//arrange
			var passwordHash = IdentityExample.ValidPasswordHash;
			var salt = IdentityExample.ValidSalt;

			//act
			Action createIdentity = () => Identity.Create(username, passwordHash, salt);

			//assert
			createIdentity.Should().Throw<InvalidIdentityUsernameException>()
				.WithMessage($"Identity username: {username} is invalid. {InvalidIdentityUsernameException.Requirements}");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void CreateIdentityWithEmptyHash_ThrowException(string passwordHash)
		{
			//arrange
			var username = IdentityExample.ValidUsername;
			var salt = IdentityExample.ValidSalt;

			//act
			Action createIdentity = () => Identity.Create(username, passwordHash, salt);

			//assert
			createIdentity.Should().Throw<EmptyIdentityPasswordHashException>()
				.WithMessage("Identity password hash can not be empty.");
		}

		[Fact]
		public void CreateIdentityWithSaltThatDoesNotExist_ThrowException()
		{
			//arrange
			var username = IdentityExample.ValidUsername;
			var passwordHash = IdentityExample.ValidPasswordHash;
			var salt = (byte[])null;

			//act
			Action createIdentity = () => Identity.Create(username, passwordHash, salt);

			//assert
			createIdentity.Should().Throw<EmptyIdentitySaltException>()
				.WithMessage("Identity salt can not be empty.");
		}

		[Fact]
		public void CreateIdentityWithEmptySalt_ThrowException()
		{
			//arrange
			var username = IdentityExample.ValidUsername;
			var passwordHash = IdentityExample.ValidPasswordHash;
			var salt = Array.Empty<byte>();

			//act
			Action createIdentity = () => Identity.Create(username, passwordHash, salt);

			//assert
			createIdentity.Should().Throw<EmptyIdentitySaltException>()
				.WithMessage("Identity salt can not be empty.");
		}
	}
}
