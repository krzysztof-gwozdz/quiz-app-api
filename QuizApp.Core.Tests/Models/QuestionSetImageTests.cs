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
	public class QuestionSetImageTests
	{
		[Theory]
		[InlineData(ContentTypes.Image.Jpeg)]
		[InlineData(ContentTypes.Image.Png)]
		[InlineData(ContentTypes.Image.Gif)]
		public void CreateQuestionSetImageWithCorrectValues_QuestionSetImageCreated(string contentType)
		{
			//arrange
			var data = QuestionSetImageExample.ValidData;

			//act
			var questionSet = QuestionSetImage.Create(data, contentType);

			//assert
			questionSet.Id.Should().NotBeEmpty();
			questionSet.Data.Should().NotBeNull();
			questionSet.ContentType.Should().BeEquivalentTo(contentType);
		}

		[Fact]
		public void CreateQuestionSetImageWithEmptyData_ThrowException()
		{
			//arrange
			Stream data = null;
			var contentType = QuestionSetImageExample.ValidContentType;

			//act
			Action createQuestionSet = () => QuestionSetImage.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetImageException>()
				.WithMessage("Question set image can not be empty.");
		}

		[Fact]
		public void CreateQuestionSetImageSizeOfDataEqual0_ThrowException()
		{
			//arrange
			var data = new MemoryStream();
			var contentType = QuestionSetImageExample.ValidContentType;

			//act
			Action createQuestionSet = () => QuestionSetImage.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetImageException>()
				.WithMessage("Question set image can not be empty.");
		}

		[Fact]
		public void CreateQuestionSetImageLargerThanMaxSize_ThrowException()
		{
			//arrange
			var data = new MemoryStream(new byte[QuestionSetImage.MaxImageSize + 1]);
			var contentType = QuestionSetImageExample.ValidContentType;

			//act
			Action createQuestionSet = () => QuestionSetImage.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<QuestionSetImageIsToLargeException>()
				.WithMessage($"Question set image is to large: {QuestionSetImage.MaxImageSize + 1}. Max image size: {QuestionSetImage.MaxImageSize}");
		}

		[Theory]
		[InlineData(ContentTypes.Text.Plain)]
		[InlineData(ContentTypes.Application.Pdf)]
		[InlineData(ContentTypes.Application.Json)]
		public void CreateQuestionSetImageWrongContentType_ThrowException(string contentType)
		{
			//arrange
			var data = QuestionSetImageExample.ValidData;

			//act
			Action createQuestionSet = () => QuestionSetImage.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<InvalidContentTypeException>()
				.WithMessage($"Invalid content type: {contentType}. Expected: {string.Join(", ", QuestionSetImage.ValidContentTypes)}.");
		}

		[Fact]
		public void CreateQuestionSetStreamThatIsNotValidImage_ThrowException()
		{
			//arrange
			var data = new MemoryStream(new byte[] { 0, 1 });
			var contentType = QuestionSetImageExample.ValidContentType;

			//act
			Action createQuestionSet = () => QuestionSetImage.Create(data, contentType);

			//assert
			createQuestionSet.Should().Throw<UploadedDataIsNotImageException>()
				.WithMessage("Uploaded data is not an image.");
		}
	}
}
