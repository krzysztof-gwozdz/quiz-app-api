using FluentAssertions;
using NSubstitute;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using QuizApp.Application.Tests.Examples;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Core.Tests.Examples;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Services
{
	public class QuestionsServiceTests
	{
		private readonly IQuestionsRepository _questionsRepository;
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly ITagsRepository _tagsRepository;
		private readonly QuestionsService _questionsService;

		public QuestionsServiceTests()
		{
			_questionsRepository = Substitute.For<IQuestionsRepository>();
			_questionSetsRepository = Substitute.For<IQuestionSetsRepository>();
			_tagsRepository = Substitute.For<ITagsRepository>();
			_questionsService = new QuestionsService(_questionsRepository, _questionSetsRepository, _tagsRepository);
		}

		[Fact]
		public async Task GetQuestions_Questions()
		{
			//arrange
			var questionsCollection = QuestionExample.GetValidQuestions(3, 4);
			_questionsRepository.GetAllAsync().Returns(questionsCollection);

			//act 
			var questions = await _questionsService.GetCollectionAsync();

			//assert
			questions.Collection.Should().HaveCount(questionsCollection.Count);
		}

		[Fact]
		public async Task GetQuestionThatExists_Question()
		{
			//arrange
			var existingQuestion = QuestionExample.GetValidQuestion(4);
			_questionsRepository.GetByIdAsync(existingQuestion.Id).Returns(existingQuestion);

			//act 
			var question = await _questionsService.GetAsync(existingQuestion.Id);

			//assert
			question.Id.Should().Be(existingQuestion.Id);
		}

		[Fact]
		public async Task GetQuestionThatDoesNotExist_ThrowException()
		{
			//arrange
			var questionId = QuestionExample.ValidId;
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
			var createQuestionDto = CreateQuestionDtoExample.ValidDto;
			_questionSetsRepository.ExistsAsync(createQuestionDto.QuestionSetId).Returns(true);
			_tagsRepository.GetByNameAsync(createQuestionDto.Tags.First()).Returns(new Tag(createQuestionDto.Tags.First(), string.Empty));

			//act 
			var questionId = await _questionsService.CreateAsync(createQuestionDto);

			//assert
			questionId.Should().NotBeEmpty();
		}

		[Fact]
		public async Task CreateQuestionWithQuestionSetIdThatDoesNotExist_ThrowException()
		{
			//arrange
			var createQuestionDto = CreateQuestionDtoExample.ValidDto;
			_questionSetsRepository.ExistsAsync(createQuestionDto.QuestionSetId).Returns(false);

			//act 
			Func<Task> createQuestion = async () => await _questionsService.CreateAsync(createQuestionDto);

			//assert
			await createQuestion.Should().ThrowAsync<QuestionSetNotFoundException>()
				.WithMessage($"Question set: {createQuestionDto.QuestionSetId} not found.");
		}

		[Fact]
		public async Task CreateQuestionWithTagThatDoesNotExist_ThrowException()
		{
			//arrange
			var text = CreateQuestionDtoExample.ValidText;
			var answers = CreateQuestionDtoExample.ValidAnswers;
			var questionSetId = CreateQuestionDtoExample.ValidQuestionSetId;
			var tagName = "Test tag";
			var tags = new[] { tagName };
			var createQuestionDto = new CreateQuestionDto(text, answers, questionSetId, tags);
			_questionSetsRepository.ExistsAsync(createQuestionDto.QuestionSetId).Returns(true);

			//act 
			Func<Task> createQuestion = async () => await _questionsService.CreateAsync(createQuestionDto);

			//assert
			await createQuestion.Should().ThrowAsync<TagNotFoundException>()
				.WithMessage($"Tag: {tagName} not found.");
		}

		[Fact]
		public async Task RemoveQuestionThatExists_QuestionRemoved()
		{
			//arrange
			var questionId = QuestionExample.ValidId;
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
			var questionId = QuestionExample.ValidId;
			_questionsRepository.ExistsAsync(questionId).Returns(false);

			//act 
			Func<Task> removeQuestion = async () => await _questionsService.RemoveAsync(questionId);

			//assert
			await removeQuestion.Should().ThrowAsync<QuestionNotFoundException>()
				.WithMessage($"Question: {questionId} not found.");
		}
	}
}
