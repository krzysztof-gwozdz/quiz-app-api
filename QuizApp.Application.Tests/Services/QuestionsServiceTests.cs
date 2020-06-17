using FluentAssertions;
using NSubstitute;
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

namespace QuizApp.Application.Tests.Services
{
	public class QuestionsServiceTests
	{
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly IQuestionsRepository _questionsRepository;
		private readonly QuestionsService _questionsService;

		public QuestionsServiceTests()
		{
			_questionSetsRepository = Substitute.For<IQuestionSetsRepository>();
			_questionsRepository = Substitute.For<IQuestionsRepository>();
			_questionsService = new QuestionsService(_questionsRepository, _questionSetsRepository);
		}

		[Fact]
		public async Task GetQuestionThatExists_Question()
		{
			//arrange
			var questionId = QuestionExample.NewId;
			_questionsRepository.GetByIdAsync(questionId).Returns(new Question(questionId, "", new HashSet<Question.Answer>(), Guid.NewGuid()));

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
			_questionsRepository.GetByIdAsync(questionId).Returns((Question)null);

			//act 
			Func<Task> getQuestion = async () => await _questionsService.GetAsync(questionId);

			//assert
			await getQuestion.Should().ThrowAsync<QuestionNotFoundException>()
				.WithMessage($"Question: {questionId} not found.");
		}

		[Fact]
		public async Task CreateQuestionCorrectValues_QuestionCreated()
		{
			//arrange
			var dto = CreateQuestionDtoExample.ValidDto;
			_questionSetsRepository.ExistsAsync(dto.QuestionSetId).Returns(true);

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
			_questionSetsRepository.ExistsAsync(dto.QuestionSetId).Returns(false);

			//act 
			Func<Task> createQuestion = async () => await _questionsService.CreateAsync(dto);

			//assert
			await createQuestion.Should().ThrowAsync<QuestionSetNotFoundException>()
				.WithMessage($"Question set: {dto.QuestionSetId} not found.");
		}

		[Fact]
		public async Task RemoveQuestionThatExists_QuestionRemoved()
		{
			//arrange
			var questionId = QuestionExample.NewId;
			_questionsRepository.ExistsAsync(questionId).Returns(true);

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
			_questionsRepository.ExistsAsync(questionId).Returns(false);

			//act 
			Func<Task> removeQuestion = async () => await _questionsService.RemoveAsync(questionId);

			//assert
			await removeQuestion.Should().ThrowAsync<QuestionNotFoundException>()
				.WithMessage($"Question: {questionId} not found.");
		}
	}
}
