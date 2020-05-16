using FluentAssertions;
using Moq;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Factories;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace QuizApp.Core.Tests.Factories
{
	public class QuizFactoryTests
	{
		private readonly Mock<IQuestionsRepository> _questionsRepositoryMock;
		private readonly Mock<IQuestionSetsRepository> _questionSetsRepositoryMock;
		private readonly Mock<IRandomFactory> _randomFactory;
		private readonly QuizFactory _quizFactory;

		public QuizFactoryTests()
		{
			_questionsRepositoryMock = new Mock<IQuestionsRepository>();
			_questionSetsRepositoryMock = new Mock<IQuestionSetsRepository>();
			_randomFactory = new Mock<IRandomFactory>();
			_quizFactory = new QuizFactory(_questionsRepositoryMock.Object, _questionSetsRepositoryMock.Object, _randomFactory.Object);
		}

		[Fact]
		public async Task CreateQuizWithCorrectValues()
		{
			//arrange
			Guid questionSetId = Guid.NewGuid();
			int questionCount = 3;
			var question = new Question(
					Guid.NewGuid(),
					"test answer 1",
					new[]
						{
							new Question.Answer(Guid.NewGuid(), "test answer 1"),
							new Question.Answer(Guid.NewGuid(), "test answer 2"),
						}.ToHashSet(),
					Guid.NewGuid(),
					Guid.NewGuid()
					);
			var questions = new[] { question }.ToHashSet();
			_questionsRepositoryMock
				.Setup(x => x.GetAllBySetIdAsync(questionSetId))
				.ReturnsAsync(questions);
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(true);
			_questionsRepositoryMock
				.Setup(x => x.CountBySetIdAsync(questionSetId))
				.ReturnsAsync(questionCount + 1);
			_randomFactory
				.Setup(x => x.NextInt(questionCount))
				.Returns(0);

			//act
			var quiz = await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			quiz.Id.Should().NotBeEmpty();
			quiz.Questions.Should().HaveCount(questionCount);
		}

		[Fact]
		public async Task CreateQuizWithQuestionSetThatDoesNotExists_ThrowException()
		{
			//arrange
			Guid questionSetId = Guid.NewGuid();
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(false);

			//act
			Func<Task> getQuiz = async () => await _quizFactory.GetAsync(questionSetId, 2);

			//assert
			await getQuiz.Should().ThrowAsync<QuestionSetDoesNotExistException>()
				.WithMessage($"Question set: {questionSetId} does not exist.");
		}

		[Fact]
		public async Task CreateQuizWithLessThanMinimumQuestions_ThrowException()
		{
			//arrange
			Guid questionSetId = Guid.NewGuid();
			int questionCount = 1;
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(true);

			//act
			Func<Task> getQuiz = async () => await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			await getQuiz.Should().ThrowAsync<NotEnoughQuestionsException>()
				.WithMessage($"Not enough question: {questionCount}. Min question count: {QuizFactory.MinQuestionCount}.");
		}

		[Fact]
		public async Task CreateQuizWithMoreThanMaxNumberOfQuestionsForQuestionSet_ThrowException()
		{
			//arrange
			Guid questionSetId = Guid.NewGuid();
			int questionCount = 11;
			int maxQuestionCount = 10;
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(true);
			_questionsRepositoryMock
				.Setup(x => x.CountBySetIdAsync(questionSetId))
				.ReturnsAsync(maxQuestionCount);

			//act
			Func<Task> getQuiz = async () => await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			await getQuiz.Should().ThrowAsync<TooManyQuestionsException>()
				.WithMessage($"Too many questions: {questionCount}. Max question count for this question set: {maxQuestionCount}.");
		}
	}
}
