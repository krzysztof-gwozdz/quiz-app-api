using FluentAssertions;
using NSubstitute;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Factories;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Core.Tests.Examples;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Services
{
	public class QuizzesServiceTests
	{
		private readonly IQuizzesRepository _quizzesRepository;
		private readonly IQuizFactory _questionsFactory;
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly QuizzesService _quizzesService;

		public QuizzesServiceTests()
		{
			_quizzesRepository = Substitute.For<IQuizzesRepository>();
			_questionsFactory = Substitute.For<IQuizFactory>();
			_questionSetsRepository = Substitute.For<IQuestionSetsRepository>();
			_quizzesService = new QuizzesService(_quizzesRepository, _questionsFactory, _questionSetsRepository);
		}

		[Fact]
		public async Task GetQuizThatExists_Quiz()
		{
			//arrange
			var existingQuiz = QuizExample.GetValidQuiz(4, 4);
			var questionSet = QuestionSetExample.ValidQuestionSet;
			_quizzesRepository.GetByIdAsync(existingQuiz.Id).Returns(existingQuiz);
			_questionSetsRepository.GetByIdAsync(existingQuiz.QuestionSetId).Returns(questionSet);

			//act 
			var quiz = await _quizzesService.GetAsync(existingQuiz.Id);

			//assert
			quiz.Id.Should().Be(existingQuiz.Id);
			quiz.QuestionSetId.Should().Be(existingQuiz.QuestionSetId);
			quiz.QuestionSetName.Should().Be(questionSet.Name);
			quiz.Questions.Should().HaveCount(existingQuiz.Questions.Count);
		}

		[Fact]
		public async Task GetQuizThatDoesNotExist_ThrowException()
		{
			//arrange
			var quizId = QuizExample.ValidId;
			_quizzesRepository.GetByIdAsync(quizId).Returns((Quiz)null);

			//act 
			Func<Task> getQuiz = async () => await _quizzesService.GetAsync(quizId);

			//assert
			await getQuiz.Should().ThrowAsync<QuizNotFoundException>()
				.WithMessage($"Quiz: {quizId} not found.");
		}

		[Fact]
		public async Task GetQuizReletedToQuizSetThatDoesNotExist_ThrowException()
		{
			//arrange
			var quiz = QuizExample.GetValidQuiz(4, 4);
			_quizzesRepository.GetByIdAsync(quiz.Id).Returns(quiz);
			_questionSetsRepository.GetByIdAsync(quiz.QuestionSetId).Returns((QuestionSet)null);

			//act 
			Func<Task> getQuiz = async () => await _quizzesService.GetAsync(quiz.Id);

			//assert
			await getQuiz.Should().ThrowAsync<QuestionSetNotFoundException>()
				.WithMessage($"Question set: {quiz.QuestionSetId} not found.");
		}

		[Fact]
		public async Task GenerateQuiz_NewQuizId()
		{
			//arrange
			var quizParameters = new QuizParametersDto(QuestionSetExample.ValidId, 4);
			_questionsFactory.GetAsync(quizParameters.QuestionSetId, quizParameters.QuestionCount).Returns(QuizExample.GetValidQuiz(4, 4));

			//act 
			var quizId = await _quizzesService.GenerateAsync(quizParameters);

			//assert
			quizId.Should().NotBeEmpty();
		}

		[Fact]
		public async Task SolveQuizThatDoesNotExist_ThrowException()
		{
			//arrange
			var solveQuizDto = new SolvedQuizDto
			{
				QuizId = QuizExample.ValidId,
				PlayerAnswers = new[]
				{
					new PlayerAnswerDto(QuizExample.PlayerAnswer.NewQuestionId, QuizExample.PlayerAnswer.NewAnswerId, (int?)QuizExample.PlayerAnswer.ValidRating)
				}
			};
			_quizzesRepository.GetByIdAsync(solveQuizDto.QuizId).Returns((Quiz)null);

			//act 
			Func<Task> getQuiz = async () => await _quizzesService.SolveAsync(solveQuizDto);

			//assert
			await getQuiz.Should().ThrowAsync<QuizNotFoundException>()
				.WithMessage($"Quiz: {solveQuizDto.QuizId} not found.");
		}

		[Fact]
		public async Task GetQuizSummaryThatExists_Quiz()
		{
			//arrange
			var existingQuiz = QuizExample.GetValidQuiz(4, 4);
			var questionSet = QuestionSetExample.ValidQuestionSet;
			_quizzesRepository.GetByIdAsync(existingQuiz.Id).Returns(existingQuiz);
			_questionSetsRepository.GetByIdAsync(existingQuiz.QuestionSetId).Returns(questionSet);

			//act 
			var quizSummary = await _quizzesService.GetSummaryAsync(existingQuiz.Id);

			//assert
			quizSummary.QuizId.Should().Be(existingQuiz.Id);
			quizSummary.QuestionSummaries.Should().HaveCount(existingQuiz.Questions.Count);
		}

		[Fact]
		public async Task GetQuizSummaryThatDoesNotExist_ThrowException()
		{
			//arrange
			var quizId = QuizExample.ValidId;
			_quizzesRepository.GetByIdAsync(quizId).Returns((Quiz)null);

			//act 
			Func<Task> getQuizSummary = async () => await _quizzesService.GetSummaryAsync(quizId);

			//assert
			await getQuizSummary.Should().ThrowAsync<QuizNotFoundException>()
				.WithMessage($"Quiz: {quizId} not found.");
		}

		[Fact]
		public async Task GetQuizSummaryReletedToQuizSetThatDoesNotExist_ThrowException()
		{
			//arrange
			var quiz = QuizExample.GetValidQuiz(4, 4);
			_quizzesRepository.GetByIdAsync(quiz.Id).Returns(quiz);
			_questionSetsRepository.GetByIdAsync(quiz.QuestionSetId).Returns((QuestionSet)null);

			//act 
			Func<Task> getQuiz = async () => await _quizzesService.GetAsync(quiz.Id);

			//assert
			await getQuiz.Should().ThrowAsync<QuestionSetNotFoundException>()
				.WithMessage($"Question set: {quiz.QuestionSetId} not found.");
		}
	}
}
