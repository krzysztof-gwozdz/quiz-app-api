using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using System;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class ColorTests
	{
		[Theory]
		[InlineData("#FF00FF")]
		[InlineData("#aa00FF")]
		[InlineData("#Fd00FF")]
		[InlineData("#123456")]
		[InlineData("#987698")]
		[InlineData("#abcdef")]
		[InlineData("#123")]
		[InlineData("#acf")]
		[InlineData("#BEA")]
		[InlineData("#987")]
		[InlineData("#564")]
		public void CreateColorWithCorrectValue_ColorCreated(string value)
		{
			//arrange

			//act
			var color = Color.Create(value);

			//assert
			color.Value.Should().Be(value);
		}

		[Theory]
		[InlineData("#QRYZAS")]
		[InlineData("FFFFFF")]
		[InlineData("#FFFFF")]
		[InlineData("#FFFF")]
		[InlineData("#FF")]
		[InlineData("#F")]
		[InlineData("#FFFFFQ")]
		public void CreateColorWithIncorrectValue_ThrownException(string value)
		{
			//arrange

			//act
			Action createColor = () => Color.Create(value);

			//assert
			createColor.Should().Throw<InvalidColorException>()
				.WithMessage($"Color: {value} is invalid.");
		}
	}
}
