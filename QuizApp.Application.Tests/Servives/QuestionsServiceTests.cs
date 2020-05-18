using FluentAssertions;
using Moq;
using QuizApp.Application.Services;
using QuizApp.Application.Tests.Examples;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Core.Tests.Examples;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Servives
{
	public class QuestionsServiceTests
	{
		private readonly Mock<IQuestionSetsRepository> _questionSetsRepositoryMock;
		private readonly Mock<IQuestionsRepository> _questionsRepositoryMock;
		private readonly QuestionsService _questionsService;

		public QuestionsServiceTests()
		{
			_questionSetsRepositoryMock = new Mock<IQuestionSetsRepository>();
			_questionsRepositoryMock = new Mock<IQuestionsRepository>();
			_questionsService = new QuestionsService(_questionsRepositoryMock.Object, _questionSetsRepositoryMock.Object);
		}

		[Fact]
		public async Task GetQuestionThatExists_Question()
		{
			//arrange
			var questionId = QuestionExample.NewId;
			_questionsRepositoryMock
				.Setup(x => x.GetByIdAsync(questionId))
				.ReturnsAsync(new Question(questionId, "", new HashSet<Question.Answer>(), Guid.NewGuid(), Guid.NewGuid()));

			//act 
			var question = await _questionsService.GetAsync(questionId);

			//assert
			question.Id.Should().Be(questionId);
		}

		[Fact]
		public async Task GetQuestionThatDoesNotExist_ThrowException()
		{
			//arrange
			var questionId = QuestionExample.NewId;
			_questionsRepositoryMock
				.Setup(x => x.GetByIdAsync(questionId))
				.ReturnsAsync((Question)null);

			//act 
			Func<Task> getQuestion = async () => await _questionsService.GetAsync(questionId);

			//assert
			await getQuestion.Should().ThrowAsync<QuestionDoesNotExistException>()
				.WithMessage($"Question: {questionId} does not exist.");
		}

		[Fact]
		public async Task CreateQuestionCorrectValues_QuestionCreated()
		{
			//arrange
			var dto = CreateQuestionDtoExample.ValidDto;
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(dto.QuestionSetId))
				.ReturnsAsync(true);

			//act 
			var questionId = await _questionsService.CreateAsync(dto);

			//assert
			questionId.Should().NotBeEmpty();
		}

		[Fact]
		public async Task CreateQuestionWithQuestionSetIdThatDoesNotExist_ThrowException()
		{
			//arrange
			var dto = CreateQuestionDtoExample.ValidDto;
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(dto.QuestionSetId))
				.ReturnsAsync(false);

			//act 
			Func<Task> createQuestion = async () => await _questionsService.CreateAsync(dto);

			//assert
			await createQuestion.Should().ThrowAsync<QuestionSetDoesNotExistException>()
				.WithMessage($"Question set: {dto.QuestionSetId} does not exist.");
		}

		[Fact]
		public async Task RemoveQuestionThatExists_QuestionRemoved()
		{
			//arrange
			var questionId = QuestionExample.NewId;
			_questionsRepositoryMock
				.Setup(x => x.ExistsAsync(questionId))
				.ReturnsAsync(true);

			//act 
			Func<Task> removeQuestion = async () => await _questionsService.RemoveAsync(questionId);

			//assert
			await removeQuestion.Should().NotThrowAsync();
		}

		[Fact]
		public async Task RemoveQuestionThatDoesNotExist_ThrowException()
		{
			//arrange
			var questionId = QuestionExample.NewId;
			_questionsRepositoryMock
				.Setup(x => x.ExistsAsync(questionId))
				.ReturnsAsync(false);

			//act 
			Func<Task> removeQuestion = async () => await _questionsService.RemoveAsync(questionId);

			//assert
			await removeQuestion.Should().ThrowAsync<QuestionDoesNotExistException>()
				.WithMessage($"Question: {questionId} does not exist.");
		}
	}
}
