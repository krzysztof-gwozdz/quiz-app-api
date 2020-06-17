using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
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
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly IQuestionsRepository _questionsRepository;
		private readonly IQuestionSetImagesRepository _questionSetImagesRepository;
		private readonly QuestionSetsService _questionSetsService;

		public QuestionSetsServiceTests()
		{
			_questionSetsRepository = Substitute.For<IQuestionSetsRepository>();
			_questionsRepository = Substitute.For<IQuestionsRepository>();
			_questionSetImagesRepository = Substitute.For<IQuestionSetImagesRepository>();
			_questionSetsService = new QuestionSetsService(_questionSetsRepository, _questionsRepository, _questionSetImagesRepository);
		}

		[Fact]
		public async Task GetQuestionSets_QuestionSets()
		{
			//arrange
			var questionsSetsCollection = new[] { QuestionSetExample.ValidQuestionSet, QuestionSetExample.ValidQuestionSet, QuestionSetExample.ValidQuestionSet };
			_questionSetsRepository.GetAllAsync().Returns(questionsSetsCollection);

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
			_questionSetsRepository.GetByIdAsync(existingQuestionSet.Id).Returns(existingQuestionSet);

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
			_questionSetsRepository.GetByIdAsync(questionSetId).Returns((QuestionSet)null);

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
			var image = Substitute.For<IFormFile>();
			image.OpenReadStream().Returns(QuestionSetImageExample.ValidData);
			image.Length.Returns(QuestionSetImageExample.ValidData.Length);
			image.ContentType.Returns(QuestionSetImageExample.ValidContentType);
			var color = QuestionSetExample.ValidColor;
			var dto = new CreateQuestionSetDto(name, description, image, color.Value);

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
			var image = Substitute.For<IFormFile>();
			var color = QuestionSetExample.ValidColor;
			var questionSet = new QuestionSet(QuestionSetExample.NewId, name, QuestionSetExample.ValidDescription, QuestionSetExample.ValidImageId, QuestionSetExample.ValidColor);
			_questionSetsRepository.GetByNameAsync(name).Returns(questionSet);
			var dto = new CreateQuestionSetDto(name, description, image, color.Value);

			//act 
			Func<Task> createQuestionSet = async () => await _questionSetsService.CreateAsync(dto);

			//assert
			await createQuestionSet.Should().ThrowAsync<QuestionSetWithSelectedNameAlreadyExistsException>()
				.WithMessage($"Question set with name: {name} already exists.");
		}
	}
}
