using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using System;
using System.IO;
using QuizApp.Shared;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class QuestionSetIconTests
	{
		[Theory]
		[InlineData(MediaTypes.Image.Jpeg)]
		[InlineData(MediaTypes.Image.Png)]
		[InlineData(MediaTypes.Image.Gif)]
		public void CreateQuestionSetIconWithCorrectValues_QuestionSetIconCreated(string contentType)
		{
			//arrange
			var data = QuestionSetIconExample.ValidData;

			//act
			var questionSet = QuestionSetIcon.Create(data, contentType);

			//assert
			questionSet.Id.Should().NotBeEmpty();
			questionSet.Data.Should().NotBeNull();
		}

		[Fact]
		public void CreateQuestionSetIconWithEmptyData_ThrowException()
		{
			//arrange
			Stream data = null;
			var contentType = QuestionSetIconExample.ValidContentType;

			//act
			Action createQuestionSet = () => QuestionSetIcon.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetIconException>()
				.WithMessage("Question set icon can not be empty.");
		}

		[Fact]
		public void CreateQuestionSetIconSizeOfDataEqual0_ThrowException()
		{
			//arrange
			var data = new MemoryStream();
			var contentType = QuestionSetIconExample.ValidContentType;

			//act
			Action createQuestionSet = () => QuestionSetIcon.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetIconException>()
				.WithMessage("Question set icon can not be empty.");
		}

		[Fact]
		public void CreateQuestionSetIconLargerThanMaxSize_ThrowException()
		{
			//arrange
			var data = new MemoryStream(new byte[QuestionSetIcon.MaxImageSize + 1]);
			var contentType = QuestionSetIconExample.ValidContentType;

			//act
			Action createQuestionSet = () => QuestionSetIcon.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<QuestionSetIconIsToLargeException>()
				.WithMessage($"Question set icon is to large: {QuestionSetIcon.MaxImageSize + 1}. Max image size: {QuestionSetIcon.MaxImageSize}");
		}

		[Theory]
		[InlineData(MediaTypes.Text.Plain)]
		[InlineData(MediaTypes.Application.Pdf)]
		[InlineData(MediaTypes.Application.Json)]
		public void CreateQuestionSetIconWrongContentType_ThrowException(string contentType)
		{
			//arrange
			var data = QuestionSetIconExample.ValidData;

			//act
			Action createQuestionSet = () => QuestionSetIcon.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<InvalidMediaTypeException>()
				.WithMessage($"Invalid media type: {contentType}. Expected: {string.Join(", ", QuestionSetIcon.ValidMediaTypes)}.");
		}
	}
}
