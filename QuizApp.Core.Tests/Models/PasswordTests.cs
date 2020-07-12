using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using System;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class PasswordTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		[InlineData("aA1$")]
		[InlineData("123456789012345678901234567890123456789012345678901234567890aA$$$")]
		[InlineData("Password1")]
		[InlineData("Password$")]
		[InlineData("password$1")]
		[InlineData("Password€1")]
		public void CreatePasswordInvalidValue_ThrowException(string value)
		{
			//arrange

			//act
			Action createPassword = () => Password.Create(value);

			//assert
			createPassword.Should().Throw<InvalidPasswordException>()
				.WithMessage($"Invalid password. {InvalidPasswordException.Requirements}");
		}
	}
}
