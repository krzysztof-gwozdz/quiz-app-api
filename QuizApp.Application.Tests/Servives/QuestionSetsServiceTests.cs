using FluentAssertions;
using Moq;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Servives
{
	public class QuestionSetsServiceTests
	{
		private readonly Mock<IQuestionSetsRepository> _questionSetsRepositoryMock;
		private readonly Mock<IQuestionsRepository> _questionsRepositoryMock;
		private readonly QuestionSetsService _questionSetsService;

		public QuestionSetsServiceTests()
		{
			_questionSetsRepositoryMock = new Mock<IQuestionSetsRepository>();
			_questionsRepositoryMock = new Mock<IQuestionsRepository>();
			_questionSetsService = new QuestionSetsService(_questionSetsRepositoryMock.Object, _questionsRepositoryMock.Object);
		}

		[Fact]
		public async Task GetQuestionSets_QuestionSets()
		{
			//arrange
			var questionsSetsCollection = new[]
				{
					new QuestionSet(Guid.NewGuid(), "test name", "", ""),
					new QuestionSet(Guid.NewGuid(), "test name", "", ""),
					new QuestionSet(Guid.NewGuid(), "test name", "", "")
				};
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
			var questionSetId = Guid.NewGuid();
			_questionSetsRepositoryMock
				.Setup(x => x.GetByIdAsync(questionSetId))
				.ReturnsAsync(new QuestionSet(questionSetId, "test name", "", ""));

			//act 
			var questionSet = await _questionSetsService.GetAsync(questionSetId);

			//assert
			questionSet.Id.Should().Be(questionSetId);
		}

		[Fact]
		public async Task GetQuestionSetThatDoesNotExist_ThrowException()
		{
			//arrange
			var questionSetId = Guid.NewGuid();
			_questionSetsRepositoryMock
				.Setup(x => x.GetByIdAsync(questionSetId))
				.ReturnsAsync((QuestionSet)null);

			//act 
			Func<Task> getQuestionSet = async () => await _questionSetsService.GetAsync(questionSetId);

			//assert
			await getQuestionSet.Should().ThrowAsync<QuestionSetDoesNotExistException>()
				.WithMessage($"Question set: {questionSetId} does not exist.");
		}

		[Fact]
		public async Task CreateQuestionSetWithUniqueName()
		{
			//arrange
			string name = Guid.NewGuid().ToString();
			var dto = new CreateQuestionSetDto { Name = name };

			//act 
			var questionSetId = await _questionSetsService.CreateAsync(dto);

			//assert
			questionSetId.Should().NotBeEmpty();
		}

		[Fact]
		public async Task CreateQuestionSetWithNameOfAnotherQuestionSet_ThrowException()
		{
			//arrange
			string name = Guid.NewGuid().ToString();
			_questionSetsRepositoryMock
				.Setup(x => x.GetByNameAsync(name))
				.ReturnsAsync(new QuestionSet(Guid.NewGuid(), name, "", ""));
			var dto = new CreateQuestionSetDto { Name = name };

			//act 
			Func<Task> createQuestionSet = async () => await _questionSetsService.CreateAsync(dto);

			//assert
			await createQuestionSet.Should().ThrowAsync<QuestionSetWithSelectedNameAlreadyExistsException>()
				.WithMessage($"Question set with name: {name} already exists.");
		}
	}
}
