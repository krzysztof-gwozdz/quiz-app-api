using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Core.Tests.Examples;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Services
{
	public class QuestionSetsServiceTests
	{
		private readonly Mock<IQuestionSetsRepository> _questionSetsRepositoryMock;
		private readonly Mock<IQuestionsRepository> _questionsRepositoryMock;
		private readonly Mock<IQuestionSetImagesRepository> _questionSetImagesRepositoryMock;
		private readonly QuestionSetsService _questionSetsService;

		public QuestionSetsServiceTests()
		{
			_questionSetsRepositoryMock = new Mock<IQuestionSetsRepository>();
			_questionsRepositoryMock = new Mock<IQuestionsRepository>();
			_questionSetImagesRepositoryMock = new Mock<IQuestionSetImagesRepository>();
			_questionSetsService = new QuestionSetsService(
				_questionSetsRepositoryMock.Object,
				_questionsRepositoryMock.Object,
				_questionSetImagesRepositoryMock.Object);
		}

		[Fact]
		public async Task GetQuestionSets_QuestionSets()
		{
			//arrange
			var questionsSetsCollection = new[] { QuestionSetExample.ValidQuestionSet, QuestionSetExample.ValidQuestionSet, QuestionSetExample.ValidQuestionSet };
			_questionSetsRepositoryMock
				.Setup(x => x.GetAllAsync())
				.ReturnsAsync(questionsSetsCollection);

			//act 
			var questionSets = await _questionSetsService.GetCollectionAsync();

			//assert
			questionSets.Collection.Should().HaveCount(questionsSetsCollection.Length);
		}

		[Fact]
		public async Task GetQuestionSetThatExists_Question()
		{
			//arrange
			var existingQuestionSet = QuestionSetExample.ValidQuestionSet;
			_questionSetsRepositoryMock
				.Setup(x => x.GetByIdAsync(existingQuestionSet.Id))
				.ReturnsAsync(existingQuestionSet);

			//act 
			var questionSet = await _questionSetsService.GetAsync(existingQuestionSet.Id);

			//assert
			questionSet.Id.Should().Be(existingQuestionSet.Id);
		}

		[Fact]
		public async Task GetQuestionSetThatDoesNotExist_ThrowException()
		{
			//arrange
			var questionSetId = QuestionSetExample.NewId;
			_questionSetsRepositoryMock
				.Setup(x => x.GetByIdAsync(questionSetId))
				.ReturnsAsync((QuestionSet)null);

			//act 
			Func<Task> getQuestionSet = async () => await _questionSetsService.GetAsync(questionSetId);

			//assert
			await getQuestionSet.Should().ThrowAsync<QuestionSetNotFoundException>()
				.WithMessage($"Question set: {questionSetId} not found.");
		}

		[Fact]
		public async Task CreateQuestionSetWithUniqueName_QuestionSetCreated()
		{
			//arrange
			var name = QuestionSetExample.ValidName;
			var description = QuestionSetExample.ValidDescription;
			var imageMock = new Mock<IFormFile>();
			imageMock.Setup(x => x.OpenReadStream()).Returns(QuestionSetImageExample.ValidData);
			imageMock.Setup(x => x.Length).Returns(QuestionSetImageExample.ValidData.Length);
			imageMock.Setup(x => x.ContentType).Returns(QuestionSetImageExample.ValidContentType);
			var color = QuestionSetExample.ValidColor;
			var dto = new CreateQuestionSetDto(name, description, imageMock.Object, color.Value);

			//act 
			var questionSetId = await _questionSetsService.CreateAsync(dto);

			//assert
			questionSetId.Should().NotBeEmpty();
		}

		[Fact]
		public async Task CreateQuestionSetWithNameOfAnotherQuestionSet_ThrowException()
		{
			//arrange
			var name = QuestionSetExample.ValidName;
			var description = QuestionSetExample.ValidDescription;
			var image = new Mock<IFormFile>().Object;
			var color = QuestionSetExample.ValidColor;
			_questionSetsRepositoryMock
				.Setup(x => x.GetByNameAsync(name))
				.ReturnsAsync(new QuestionSet(QuestionSetExample.NewId, name, QuestionSetExample.ValidDescription, QuestionSetExample.ValidImageId, QuestionSetExample.ValidColor));
			var dto = new CreateQuestionSetDto(name, description, image, color.Value);

			//act 
			Func<Task> createQuestionSet = async () => await _questionSetsService.CreateAsync(dto);

			//assert
			await createQuestionSet.Should().ThrowAsync<QuestionSetWithSelectedNameAlreadyExistsException>()
				.WithMessage($"Question set with name: {name} already exists.");
		}
	}
}
