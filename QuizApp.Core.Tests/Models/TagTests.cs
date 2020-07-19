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
			var description = TagExample.ValidDescription;

			//act
			var tag = Tag.Create(name, description);

			//assert
			tag.Id.Should().NotBeEmpty();
			tag.Name.Should().Be(name);
			tag.Description.Should().Be(description);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateTagWithEmptyName_ThrowException(string name)
		{
			//arrange
			var description = TagExample.ValidDescription;

			//act
			Action createTag = () => Tag.Create(name, description);

			//assert
			createTag.Should().Throw<EmptyTagNameException>()
				.WithMessage("Tag name can not be empty.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateTagWithEmptyDescription_ThrowException(string description)
		{
			//arrange
			var name = TagExample.ValidName;

			//act
			Action createTag = () => Tag.Create(name, description);

			//assert
			createTag.Should().Throw<EmptyTagDescriptionException>()
				.WithMessage("Tag description can not be empty.");
		}
	}
}
