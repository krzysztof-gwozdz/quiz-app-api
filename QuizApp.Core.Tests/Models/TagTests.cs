using FluentAssertions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using QuizApp.Shared.Exceptions;
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
			createTag.Should().Throw<ValidationException>()
				.WithMessage("name: Tag name can not be empty.");
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
			createTag.Should().Throw<ValidationException>()
				.WithMessage("description: Tag description can not be empty.");
		}
	}
}
