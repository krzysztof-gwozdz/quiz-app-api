using FluentAssertions;
using Moq;
using QuizApp.Application.Services;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Factories;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Servives
{
	public class QuizesServiceTests
	{
		private readonly Mock<IQuizesRepository> _quizesRepositoryMock;
		private readonly Mock<IQuizFactory> _questionsFactoryMock;
		private readonly QuizesService _quizesService;

		public QuizesServiceTests()
		{
			_quizesRepositoryMock = new Mock<IQuizesRepository>();
			_questionsFactoryMock = new Mock<IQuizFactory>();
			_quizesService = new QuizesService(_quizesRepositoryMock.Object, _questionsFactoryMock.Object);
		}

		[Fact]
		public async Task GetQuizThatExists_Quiz()
		{
			//arrange
			var quizId = Guid.NewGuid();
			var questions = new[]
			{
				new Quiz.Question(Guid.NewGuid(), "test question", new [] { new Quiz.Question.Answer(Guid.NewGuid(), "" )}.ToHashSet(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()),
				new Quiz.Question(Guid.NewGuid(), "test question", new [] { new Quiz.Question.Answer(Guid.NewGuid(), "" )}.ToHashSet(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()),
				new Quiz.Question(Guid.NewGuid(), "test question", new [] { new Quiz.Question.Answer(Guid.NewGuid(), "" )}.ToHashSet(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()),
			};
			_quizesRepositoryMock
				.Setup(x => x.GetByIdAsync(quizId))
				.ReturnsAsync(new Quiz(quizId, questions));

			//act 
			var quiz = await _quizesService.GetAsync(quizId);

			//assert
			quiz.Id.Should().Be(quizId);
			quiz.Questions.Should().HaveCount(questions.Length);
		}

		[Fact]
		public async Task GetQuizThatDoesNotExist_ThrowException()
		{
			//arrange
			var quizId = Guid.NewGuid();
			_quizesRepositoryMock
				.Setup(x => x.GetByIdAsync(quizId))
				.ReturnsAsync((Quiz)null);

			//act 
			Func<Task> getQuiz = async () => await _quizesService.GetAsync(quizId);

			//assert
			await getQuiz.Should().ThrowAsync<QuizDoesNotExistException>()
				.WithMessage($"Quiz: {quizId} does not exist.");
		}
	}
}
