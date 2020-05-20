using FluentAssertions;
using Moq;
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
		private readonly Mock<IQuizzesRepository> _quizzesRepositoryMock;
		private readonly Mock<IQuizFactory> _questionsFactoryMock;
		private readonly QuizzesService _quizzesService;

		public QuizzesServiceTests()
		{
			_quizzesRepositoryMock = new Mock<IQuizzesRepository>();
			_questionsFactoryMock = new Mock<IQuizFactory>();
			_quizzesService = new QuizzesService(_quizzesRepositoryMock.Object, _questionsFactoryMock.Object);
		}

		[Fact]
		public async Task GetQuizThatExists_Quiz()
		{
			//arrange
			var existingQuiz = QuizExample.GetValidQuiz(4, 4);
			_quizzesRepositoryMock
				.Setup(x => x.GetByIdAsync(existingQuiz.Id))
				.ReturnsAsync(existingQuiz);

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
			_quizzesRepositoryMock
				.Setup(x => x.GetByIdAsync(quizId))
				.ReturnsAsync((Quiz)null);

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
			var quizParameters = new QuizParametersDto
			{
				QuestionSetId = QuestionSetExample.NewId,
				QuestionCount = 4
			};
			_questionsFactoryMock
				.Setup(x => x.GetAsync(quizParameters.QuestionSetId, quizParameters.QuestionCount))
				.ReturnsAsync(QuizExample.GetValidQuiz(4, 4));

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
				PlayerAnswers = new[] { new PlayerAnswerDto { QuestionId = QuizExample.PlayerAnswer.NewQuestionId, AnswerId = QuizExample.PlayerAnswer.NewAnswerId } }
			};
			_quizzesRepositoryMock
				.Setup(x => x.GetByIdAsync(solveQuizDto.QuizId))
				.ReturnsAsync((Quiz)null);

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
			_quizzesRepositoryMock
				.Setup(x => x.GetByIdAsync(existingQuiz.Id))
				.ReturnsAsync(existingQuiz);

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
			_quizzesRepositoryMock
				.Setup(x => x.GetByIdAsync(quizId))
				.ReturnsAsync((Quiz)null);

			//act 
			Func<Task> getQuizSummary = async () => await _quizzesService.GetSummaryAsync(quizId);

			//assert
			await getQuizSummary.Should().ThrowAsync<QuizNotFoundException>()
				.WithMessage($"Quiz: {quizId} not found.");
		}
	}
}
