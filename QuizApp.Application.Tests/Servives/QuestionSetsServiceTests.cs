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
