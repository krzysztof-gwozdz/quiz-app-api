using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using System;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class TagTests
	{
		[Fact]
		public void CreateTagWithCorrectValues_TagCreated()
		{
			//arrange
			var name = TagExample.ValidName;

			//act
			var tag = Tag.Create(name);

			//assert
			tag.Id.Should().NotBeEmpty();
			tag.Name.Should().Be(name);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateTagWithEmptyName_ThrowException(string name)
		{
			//arrange

			//act
			Action createTag = () => Tag.Create(name);

			//assert
			createTag.Should().Throw<EmptyTagNameException>()
				.WithMessage("Tag name can not be empty.");
		}
	}
}
