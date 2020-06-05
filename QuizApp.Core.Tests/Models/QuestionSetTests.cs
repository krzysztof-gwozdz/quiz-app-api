using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using System;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class QuestionSetTests
	{
		[Fact]
		public void CreateQuestionSetWithCorrectValues_QuestionSetCreated()
		{
			//arrange
			var name = QuestionSetExample.ValidName;
			var description = QuestionSetExample.ValidDescription;
			var imageId = QuestionSetExample.ValidImageId;
			var color = QuestionSetExample.ValidColor;

			//act
			var questionSet = QuestionSet.Create(name, description, imageId, color);

			//assert
			questionSet.Id.Should().NotBeEmpty();
			questionSet.Name.Should().Be(name);
			questionSet.ImageId.Should().NotBeEmpty();
			questionSet.Color.Should().Be(color);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateQuestionSetWithEmptyName_ThrowException(string name)
		{
			//arrange
			var description = QuestionSetExample.ValidDescription;
			var imageId = QuestionSetExample.ValidImageId;
			var color = QuestionSetExample.ValidColor;

			//act
			Action createQuestionSet = () => QuestionSet.Create(name, description, imageId, color);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetNameException>()
				.WithMessage("Question set name can not be empty.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateQuestionSetWithEmptyDescription_ThrowException(string description)
		{
			//arrange
			var name = QuestionSetExample.ValidName;
			var imageId = QuestionSetExample.ValidImageId;
			var color = QuestionSetExample.ValidColor;

			//act
			Action createQuestionSet = () => QuestionSet.Create(name, description, imageId, color);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetDescriptionException>()
				.WithMessage("Question set description can not be empty.");
		}


		[Fact]
		public void CreateQuestionSetWithEmptyImage_ThrowException()
		{
			//arrange
			var name = QuestionSetExample.ValidName;
			var description = QuestionSetExample.ValidDescription;
			var imageId = Guid.Empty;
			var color = QuestionSetExample.ValidColor;

			//act
			Action createQuestionSet = () => QuestionSet.Create(name, description, imageId, color);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetImageException>()
				.WithMessage("Question set image can not be empty.");
		}
	}
}
