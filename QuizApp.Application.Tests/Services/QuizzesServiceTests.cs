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
		private readonly QuizzesService _quizzesService;

		public QuizzesServiceTests()
		{
			_quizzesRepository = Substitute.For<IQuizzesRepository>();
			_questionsFactory = Substitute.For<IQuizFactory>();
			_quizzesService = new QuizzesService(_quizzesRepository, _questionsFactory);
		}

		[Fact]
		public async Task GetQuizThatExists_Quiz()
		{
			//arrange
			var existingQuiz = QuizExample.GetValidQuiz(4, 4);
			_quizzesRepository.GetByIdAsync(existingQuiz.Id).Returns(existingQuiz);

			//act 
			var quiz = await _quizzesService.GetAsync(existingQuiz.Id);

			//assert
			quiz.Id.Should().Be(existingQuiz.Id);
			quiz.Questions.Should().HaveCount(existingQuiz.Questions.Count);
		}

		[Fact]
		public async Task GetQuizThatDoesNotExist_ThrowException()
		{
			//arrange
			var quizId = QuizExample.NewId;
			_quizzesRepository.GetByIdAsync(quizId).Returns((Quiz)null);

			//act 
			Func<Task> getQuiz = async () => await _quizzesService.GetAsync(quizId);

			//assert
			await getQuiz.Should().ThrowAsync<QuizNotFoundException>()
				.WithMessage($"Quiz: {quizId} not found.");
		}

		[Fact]
		public async Task GenerateQuiz_NewQuizId()
		{
			//arrange
			var quizParameters = new QuizParametersDto(QuestionSetExample.NewId, 4);
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
				QuizId = QuizExample.NewId,
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
			_quizzesRepository.GetByIdAsync(existingQuiz.Id).Returns(existingQuiz);

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
			var quizId = QuizExample.NewId;
			_quizzesRepository.GetByIdAsync(quizId).Returns((Quiz)null);

			//act 
			Func<Task> getQuizSummary = async () => await _quizzesService.GetSummaryAsync(quizId);

			//assert
			await getQuizSummary.Should().ThrowAsync<QuizNotFoundException>()
				.WithMessage($"Quiz: {quizId} not found.");
		}
	}
}
